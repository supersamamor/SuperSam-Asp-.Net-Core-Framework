using ProjectNamePlaceHolder.Application;
using ProjectNamePlaceHolder.Web.Extensions;
using ProjectNamePlaceHolder.Web.Models;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Pages.MainModulePlaceHolder
{
    public static class MainModulePlaceHolderHtmlElementVariables
    {     
        public const string MainModulePlaceHolderForm = "formMainModulePlaceHolderForm";
        public static PromptContainer MainModulePlaceHolderFormPromptContainer = new PromptContainer(name: "promptMainModulePlaceHolderForm", effects: "Blink");
        public static PromptContainer MainModulePlaceHolderListingsPromptContainer = new PromptContainer(name: "promptMainModulePlaceHolderListings", effects: "Blink");
        public const string MainModulePlaceHolderListingsContainer = "containerMainModulePlaceHolderListings";

        public static readonly FormModal MainModulePlaceHolderModal = new FormModal(name: "MainModulePlaceHolderModal", width: 700, isDraggable: true);
        public static readonly PageHandler ShowCreateHandler = new PageHandler("ShowCreate", Resource.LabelAddMainModulePlaceHolder);
        public static readonly PageHandler ShowEditHandler = new PageHandler(name: "ShowEdit", description: Resource.LabelEditMainModulePlaceHolder, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler ShowViewHandler = new PageHandler(name: "ShowView", description: Resource.LabelDetailsMainModulePlaceHolder, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler ShowDeleteHandler = new PageHandler(name: "ShowDelete", description: Resource.LabelDeleteMainModulePlaceHolder, handlerParameters: new List<string> { "id" });
        public static readonly PageHandler SaveHandler = new PageHandler(name: "Save", withPromptConfirmation: true);
        public static readonly PageHandler UpdateHandler = new PageHandler(name: "Update", withPromptConfirmation: true);
        public static readonly PageHandler DeleteHandler = new PageHandler(name: "Delete", withPromptConfirmation: true);
        public static readonly PageHandler InitializeListHandler = new PageHandler(name: "InitializeList");
    }
}

