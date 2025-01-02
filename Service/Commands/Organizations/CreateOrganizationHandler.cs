using System.Data.Entity;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Data;
using Data.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Service.Commands.Organizations;

public class CreateOrganizationHandler(SpontanizeContext context) : IRequestHandler<CreateOrganizationRequest, bool>
{
    public async Task<bool> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        CreateOrganizationValidator validator = new CreateOrganizationValidator(context);
        validator.ValidateAndThrow(request);
        
        Organization organization = new Organization()
        {
            Name = request.Name,
            Slug = ToSlug(request.Name)
        };

        context.Organization.Add(organization);
        context.SaveChanges();
        
        var user = context.Users.FirstOrDefault(user => user.Id == request.UserId);

        if (user == null)
            throw new UnauthorizedAccessException("User not found!");
        
        user.OrganizationId = organization.Id;
        context.Users.Update(user);
        
        return context.SaveChanges() > 0;
    }


    private string ToSlug(string name)
    {
        string str = RemoveAccent(name).ToLower();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str;
    }

    private string RemoveAccent(string txt)
    {
        if (string.IsNullOrEmpty(txt))
            return txt;

        string normalizedString = txt.Normalize(NormalizationForm.FormD);

        char[] result = normalizedString
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray();

        return new string(result).Normalize(NormalizationForm.FormC);
    }
}