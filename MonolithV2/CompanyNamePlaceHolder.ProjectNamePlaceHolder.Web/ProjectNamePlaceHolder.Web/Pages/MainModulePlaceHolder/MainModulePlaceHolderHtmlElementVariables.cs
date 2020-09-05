using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    public static class MainModulePlaceHolderHtmlElementVariables
    {     
        public const string MainModulePlaceHolderForm = "formMainModulePlaceHolderForm";
        public const string MainModulePlaceHolderFormPromptContainer = "promptMainModulePlaceHolderForm";
        public const string MainModulePlaceHolderListingsPromptContainer = "promptMainModulePlaceHolderListings";
        public const string MainModulePlaceHolderListingsContainer = "containerMainModulePlaceHolderListings";

        public static readonly FormModal MainModulePlaceHolderModal = new FormModal("MainModulePlaceHolderModal", 700);
        public static readonly PageHandler ShowCreateHandler = new PageHandler("ShowCreate", Resource.LabelAddMainModulePlaceHolder);
        public static readonly PageHandler ShowEditHandler = new PageHandler("ShowEdit", Resource.LabelEditMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler ShowViewHandler = new PageHandler("ShowView", Resource.LabelDetailsMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler ShowDeleteHandler = new PageHandler("ShowDelete", Resource.LabelDeleteMainModulePlaceHolder, new List<string> { "id" });
        public static readonly PageHandler SaveHandler = new PageHandler(name: "Save", withPromptConfirmation: true);
        public static readonly PageHandler UpdateHandler = new PageHandler(name: "Update", withPromptConfirmation: true);
        public static readonly PageHandler DeleteHandler = new PageHandler(name: "Delete", withPromptConfirmation: true);
        public static readonly PageHandler InitializeListHandler = new PageHandler(name: "InitializeList");
    }
}

