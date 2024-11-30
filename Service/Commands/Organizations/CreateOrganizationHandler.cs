using System.Text.RegularExpressions;
using Data;
using Data.Models;
using MediatR;

namespace Service.Commands.Organizations;

public class CreateOrganizationHandler(SpontanizeContext context) : IRequestHandler<CreateOrganizationRequest, bool>
{
    public async Task<bool> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        Organization organization = new Organization()
        {
            Name = request.Name,
            Slug = ToSlug(request.Name)
        };

        context.Organization.Add(organization);
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
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt); 
        return System.Text.Encoding.ASCII.GetString(bytes); 
    }
}