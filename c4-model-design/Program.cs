using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderModels();
        }

        static void RenderModels()
        {
            const long workspaceId = 77525;
            const string apiKey = "92cd0cb3-a737-4353-b4b4-ba0a01ea868b";
            const string apiSecret = "2d31febd-162b-4e11-8423-70cdc8b3bda0";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);

            Workspace workspace = new Workspace("Software Design & Patterns - C4 Model - Nuna Thani", "Sistema de enlace de profesionales y pacientes de la salud mental Nuna Thani");

            ViewSet viewSet = workspace.Views;

            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem Enlazador = model.AddSoftwareSystem("Sistema de tratado de la salud mental Nuna Thani", "Permite que los pacientes con problemas de salud mental puedan diagnosticar su enfermedad, recibir sugerencias para tratarla, y agendar citas con psicólogos.");
            SoftwareSystem Yape = model.AddSoftwareSystem("Yape", "Permite realizar transacciones monetarias simples a través de un código QR");
            SoftwareSystem Plin = model.AddSoftwareSystem("Plin", "Permite realizar transacciones monetarias simples a través de un código QR");
            SoftwareSystem Tunki = model.AddSoftwareSystem("Tunki", "Permite realizar transacciones monetarias simples a través de un código QR");
            SoftwareSystem Visa = model.AddSoftwareSystem("Visa", "Permite realizar transacciones monetarias de una cuenta a otra");
            SoftwareSystem GoogleMeet = model.AddSoftwareSystem("Google Meet", "Permite realizar videoconferencias");
            SoftwareSystem GoogleCalendar  = model.AddSoftwareSystem("Google Calendar", "Permite agendar citas");
            SoftwareSystem GoogleAccount = model.AddSoftwareSystem("Google Account", "Permite gestionar cuentas de correo electrónico");
            SoftwareSystem ChatBot = model.AddSoftwareSystem("ChatBot", "Permite interactuar con el paciente de forma natural");

            Person Paciente = model.AddPerson("Paciente", "Paciente con algún problema de salud mental");
            Person Psicologo = model.AddPerson("Psicólogo", "¨Profesional de la salud mental");
            Person Administrador = model.AddPerson("Administrador", "Persona encargada de administrar el sistema");

            Paciente.Uses(Enlazador, "Agenda citas psicológicas para tratar su problema de salud mental");
            Psicologo.Uses(Enlazador, "Trata a los pacientes para ayudarlos con su problema de salud mental");
            Administrador.Uses(Enlazador, "Administra el sistema");

            Enlazador.Uses(Yape, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Plin, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Tunki, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Visa, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(GoogleMeet, "Realiza videoconferencias con los pacientes");
            Enlazador.Uses(GoogleCalendar, "Agenda citas con los pacientes");
            Enlazador.Uses(GoogleAccount, "Gestiona las cuentas de correo electrónico de los pacientes");
            Enlazador.Uses(ChatBot, "Interactúa con los pacientes de forma natural");

            // Tags
            Paciente.AddTags("Paciente");
            Psicologo.AddTags("Psicologo");
            Administrador.AddTags("Administrador");
            ChatBot.AddTags("ChatBot");
            Enlazador.AddTags("NunaThani");
            Yape.AddTags("Yape");
            Plin.AddTags("Plin");
            Tunki.AddTags("Tunki");
            Visa.AddTags("Visa");
            GoogleMeet.AddTags("GoogleMeet");
            GoogleCalendar.AddTags("GoogleCalendar");
            GoogleAccount.AddTags("GoogleAccount");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Paciente") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Psicologo") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Administrador") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("NunaThani") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Yape") { Background = "#00193b", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Plin") { Background = "#00eaff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Tunki") { Background = "#f700ff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Visa") { Background = "#0008ff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleMeet") { Background = "#ff0000", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleCalendar") { Background = "#ff0000", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleAccount") { Background = "#ff0000", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("ChatBot") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });

            SystemContextView contextView = viewSet.CreateSystemContextView(Enlazador, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores
            Container mobileApplication = Enlazador.AddContainer("Mobile App", "Permite a los usuarios ver sus próximas citas, tratar sus problemas de salud mental, y editar sus datos", "Swift UI");
            Container landingPage = Enlazador.AddContainer("Landing Page", "", "React");
            Container apiRest = Enlazador.AddContainer("API REST", "API Rest", "NodeJS (NestJS) port 8080");

            Container PaymentMethod = Enlazador.AddContainer("Payment Context", "Bounded Context de cobro y pago de la aplicación, para que los pacientes agenden citas", "NodeJS (NestJS)");
            Container AppointmentContext = Enlazador.AddContainer("Appointment Context", "Bounded Context de citas de psicólogos y pacientes", "NodeJS (NestJS)");
            Container ChatBotContext = Enlazador.AddContainer("Chatbot Context", "Bounded Context de Chatbot con el paciente", "NodeJS (NestJS)");
            Container AccountContext = Enlazador.AddContainer("Account Context", "Bounded Context de Cuentas de la aplicación", "NodeJS (NestJS)");
            Container MentalHealthContext = Enlazador.AddContainer("Mental Health Diagnosis Context", "Bounded Context de diagnósticos de salud mental", "NodeJS (NestJS)");
            Container GroupsContext = Enlazador.AddContainer("Mental Care Groups Context", "Bounded Context de grupos de pacientes con enfermedades mentales", "NodeJS (NestJS)");

            Container database = Enlazador.AddContainer("Database", "", "SQL");
            
            Paciente.Uses(mobileApplication, "Consulta");
            Paciente.Uses(landingPage, "Consulta");

            Psicologo.Uses(mobileApplication, "Consulta");
            Psicologo.Uses(landingPage, "Consulta");
            
            Administrador.Uses(mobileApplication, "Consulta");
            Administrador.Uses(landingPage, "Consulta");

            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(PaymentMethod, "", "");
            apiRest.Uses(AppointmentContext, "", "");
            apiRest.Uses(ChatBotContext, "", "");
            apiRest.Uses(AccountContext, "", "");
            apiRest.Uses(MentalHealthContext, "", "");
            apiRest.Uses(GroupsContext, "", "");

            PaymentMethod.Uses(database, "", "");
            AppointmentContext.Uses(database, "", "");
            ChatBotContext.Uses(database, "", "");
            AccountContext.Uses(database, "", "");
            MentalHealthContext.Uses(database, "", "");
            GroupsContext.Uses(database, "", "");

            PaymentMethod.Uses(Yape, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Plin, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Tunki, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Visa, "API Request", "JSON/HTTPS");
            
            AppointmentContext.Uses(GoogleMeet, "API Request", "JSON/HTTPS");
            AppointmentContext.Uses(GoogleCalendar, "API Request", "JSON/HTTPS");
            
            ChatBotContext.Uses(ChatBot, "API Request", "JSON/HTTPS");
            
            AccountContext.Uses(GoogleAccount, "API Request", "JSON/HTTPS");
            

            // Tags
            mobileApplication.AddTags("MobileApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");

            string contextTag = "Context";

            PaymentMethod.AddTags(contextTag);
            AppointmentContext.AddTags(contextTag);
            ChatBotContext.AddTags(contextTag);
            AccountContext.AddTags(contextTag);
            MentalHealthContext.AddTags(contextTag);
            GroupsContext.AddTags(contextTag);

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle(contextTag) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(Enlazador, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            // 3. Diagrama de Componentes (Monitoring Context)
            Component domainLayer = PaymentMethod.AddComponent("Domain Layer", "", "NodeJS (NestJS)");

            Component monitoringController = PaymentMethod.AddComponent("PaymentController", "REST API de pago de citas.", "NodeJS (NestJS) REST Controller");

            Component monitoringApplicationService = PaymentMethod.AddComponent("DatesPaymentService", "Provee métodos para el pago de citas, pertenece a la capa Application de DDD", "NestJS Component");

            apiRest.Uses(monitoringController, "", "JSON/HTTPS");
            monitoringController.Uses(monitoringApplicationService, "Invoca métodos de monitoreo");

            monitoringApplicationService.Uses(domainLayer, "Usa", "");
            monitoringApplicationService.Uses(database, "", "");
            monitoringController.Uses(database, "", "");

            monitoringController.Uses(Yape, "", "JSON/HTTPS");
            monitoringController.Uses(Plin, "", "JSON/HTTPS");
            monitoringController.Uses(Tunki, "", "JSON/HTTPS");
            monitoringController.Uses(Visa, "", "JSON/HTTPS");
            // Tags
            domainLayer.AddTags("DomainLayer");
            monitoringController.AddTags("MonitoringController");
            monitoringApplicationService.AddTags("MonitoringApplicationService");
            
            styles.Add(new ElementStyle("DomainLayer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringDomainModel") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FlightStatus") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
      

            ComponentView componentView = viewSet.CreateComponentView(PaymentMethod, "Components", "Component Diagram");
            componentView.PaperSize = PaperSize.A4_Landscape;
            componentView.Add(mobileApplication);
            componentView.Add(apiRest);
            componentView.Add(database);
            componentView.Add(Visa);
            componentView.Add(Yape);
            componentView.Add(Tunki);
            componentView.Add(Plin);
            componentView.Add(GoogleAccount);
            componentView.Add(GoogleCalendar);
            componentView.Add(GoogleMeet);
            componentView.Add(ChatBot);
            
            componentView.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}