using GFL.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GFL.Pages
{
    public class IndexModel : PageModel
    {

        public IEnumerable<Folder> Folders { get; set; }
        private readonly FolderRepo folderRepo;

        public IndexModel(FolderRepo folderRepo)
        {
            this.folderRepo = folderRepo;
        }

        public void OnGet()
        {
            Folders = folderRepo.GetFolderHierarcy();
        }
        public void OnPost()
        {
            var file = Request.Form.Files["file"];
            if (file is null)
                return;
            using var fileToRead = file.OpenReadStream();
            using var streamReader = new StreamReader(fileToRead);
            var folderJSON = streamReader.ReadToEnd();
            var folder = JsonConvert.DeserializeObject<Folder>(folderJSON);
            folderRepo.InsertAll(folder);
        }
    }
}