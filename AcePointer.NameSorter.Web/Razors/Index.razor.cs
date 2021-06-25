using AcePointer.NameSorter.DTO;
using AcePointer.NameSorter.Repo;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcePointer.NameSorter.Web.Razors
{
    public partial class Index
    {
        [Inject]
        public IPersonRepo PersonRepo { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        const string DefaultDataSource = "./Data/unsorted-names-list.txt";
        const string DownloadEndpoint = "download/sorted-names-list.txt";

        public List<Person> Persons { get; set; } = new();
        protected SfGrid<Person> DataGrid { get; set; }

        protected override void OnInitialized()
        {
            PersonRepo.Load(DefaultDataSource);
            Persons = PersonRepo.GetSortedPersons();
        }
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id.Contains("pdf", StringComparison.InvariantCultureIgnoreCase))
            {
                await DataGrid.PdfExport();
            }
            else if (args.Item.Id.Contains("excel", StringComparison.InvariantCultureIgnoreCase))
            {
                await DataGrid.ExcelExport();
            }
            else if (args.Item.Id.Contains("text", StringComparison.InvariantCultureIgnoreCase))
            {
                NavigationManager.NavigateTo(DownloadEndpoint,true);
            }
        }


        protected void OnUpload(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                setupPersonRepo(file.Stream);
            }

            DataGrid.GoToPage(1);
        }

        private void setupPersonRepo(MemoryStream memoryStream)
        {
            PersonRepo.Load(memoryStream);

            Persons = PersonRepo.GetSortedPersons();

            StateHasChanged();
        }
    }
}
