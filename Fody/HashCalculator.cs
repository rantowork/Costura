using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Mono.Cecil;

partial class ModuleWeaver
{
    string resourcesHash;

    void CalculateHash()
    {
        var data = ModuleDefinition.Resources.OfType<EmbeddedResource>()
            .OrderBy(r => r.Name)
            .Where(r => r.Name.StartsWith("costura"))
            .SelectMany(r => r.FixedGetResourceData())
            .ToArray();

	    using (var sha1 = new SHA1CryptoServiceProvider())
	    {
			var hashBytes = sha1.ComputeHash(data);

			var sb = new StringBuilder();
			for (var i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("X2"));
			}

			resourcesHash = sb.ToString();
	    }
    }
}