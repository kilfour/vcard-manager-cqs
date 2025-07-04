
using VCardManager.CLI;
using VCardManager.Core;
using VCardManager.Core.Handlers.AddContact;
using VCardManager.Core.Handlers.DeleteContact;
using VCardManager.Core.Handlers.SearchContact;
using VCardManager.Core.Handlers.ShowAllContacts;

var console = new SystemConsole();
var fileStore = new FileSystemStore();

var receptionist = new TheReceptionist();
var inquisitor = new Inquisitor(console, receptionist);
var printer = new ThePrinter(console);
var stackOfPaper = new StackOfPaper(fileStore);

var dispatcher = new Dispatcher()
    .Register(new ShowAllContactsHandler(stackOfPaper, printer))
    .Register(new AddContactHandler(stackOfPaper, inquisitor))
    .Register(new SearchContactHandler(stackOfPaper, inquisitor, printer))
    .Register(new DeleteContactHandler(stackOfPaper, inquisitor, printer));

new Menu(dispatcher, console).Run();





