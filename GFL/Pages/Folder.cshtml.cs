using GFL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GFL.Pages
{
    public class FolderModel : PageModel
    {
        private readonly FolderRepo folderRepo;
        public FolderModel(FolderRepo folderRepo)
        {
            this.folderRepo = folderRepo;
        }

        public Folder Folder { get; set; }

        public void OnGet(Guid id)
        {
            Folder = folderRepo.GetFullFolder(id);
        }
        public ActionResult OnPost(Guid id)
        {
            OnGet(id);
            var folderJSON = JsonConvert.SerializeObject(Folder);
            var memoStream = new MemoryStream();
            var writer = new StreamWriter(memoStream);
            writer.Write(folderJSON);
            writer.Flush();
            memoStream.Position = 0;
            return File(memoStream, "application/json", $"{Folder.Name}_folder.json");
        }
    }
}
