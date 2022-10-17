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
            const long workspaceId = 77391;
            const string apiKey = "05545864-98fa-426b-a228-3e340763f5f4";
            const string apiSecret = "4fb1d5e4-4acb-4189-ba87-c0ccc3b3df63";

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

            Person Paciente = model.AddPerson("Paciente", "Paciente con algún problema de salud mental");
            Person Psicologo = model.AddPerson("Psicólogo", "¨Profesional de la salud mental");

            Paciente.Uses(Enlazador, "Agenda citas psicológicas para tratar su problema de salud mental");
            Psicologo.Uses(Enlazador, "Trata a los pacientes para ayudarlos con su problema de salud mental");

            Enlazador.Uses(Yape, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Plin, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Tunki, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");
            Enlazador.Uses(Visa, "Consulta información en tiempo real el estado y las transacciones de la cuenta de la aplicación, y paga a los psicólogos");

            Paciente.Uses(Yape, "Consulta información de su estado de cuenta y hace transferencias a la aplicación");
            Paciente.Uses(Plin, "Consulta información de su estado de cuenta y hace transferencias a la aplicación");
            Paciente.Uses(Tunki, "Consulta información de su estado de cuenta y hace transferencias a la aplicación");
            Paciente.Uses(Visa, "Consulta información de su estado de cuenta y hace transferencias a la aplicación");

            Psicologo.Uses(Yape, "Consulta información de su estado de cuenta para confirmar que se le halla pagado");
            Psicologo.Uses(Plin, "Consulta información de su estado de cuenta para confirmar que se le halla pagado");
            Psicologo.Uses(Tunki, "Consulta información de su estado de cuenta para confirmar que se le halla pagado");
            Psicologo.Uses(Visa, "Consulta información de su estado de cuenta para confirmar que se le halla pagado");

            // Tags
            Paciente.AddTags("Paciente");
            Psicologo.AddTags("Psicologo");
            Enlazador.AddTags("NunaThani");
            Yape.AddTags("Yape");
            Plin.AddTags("Plin");
            Tunki.AddTags("Tunki");
            Visa.AddTags("Visa");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Paciente") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Psicologo") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("NunaThani") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Yape") { Background = "#00eaff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Plin") { Background = "#00eaff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Tunki") { Background = "#f700ff", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Visa") { Background = "#0008ff", Color = "#ffffff", Shape = Shape.RoundedBox });

            SystemContextView contextView = viewSet.CreateSystemContextView(Enlazador, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores
            Container mobileApplication = Enlazador.AddContainer("Mobile App", "Permite a los usuarios ver sus próximas citas, tratar sus problemas de salud mental, y editar sus datos", "Swift UI");
            Container landingPage = Enlazador.AddContainer("Landing Page", "", "React");
            Container apiRest = Enlazador.AddContainer("API REST", "API Rest", "NodeJS (NestJS) port 8080");

            Container PaymentMethod = Enlazador.AddContainer("Payment Context", "Bounded Context de cobro y pago de la aplicación, para que los pacientes agenden citas", "NodeJS (NestJS)");
            Container CalendarContext = Enlazador.AddContainer("Calendar Context", "Bounded Context de calendarios de psicólogos y pacientes", "NodeJS (NestJS)");
            Container ChatBotContext = Enlazador.AddContainer("Chatbot Context", "Bounded Context de Chatbot con el paciente", "NodeJS (NestJS)");
            Container AccountContext = Enlazador.AddContainer("Account Context", "Bounded Context de Cuentas de la aplicación", "NodeJS (NestJS)");
            Container MentalHealthContext = Enlazador.AddContainer("Mental Illness Diagnosis Context", "Bounded Context de diagnósticos de salud mental", "NodeJS (NestJS)");
            Container GroupsContext = Enlazador.AddContainer("Mental Care Groups Context", "Bounded Context de grupos de pacientes con enfermedades mentales", "NodeJS (NestJS)");

            Container database = Enlazador.AddContainer("Database", "", "SQL");
            
            Paciente.Uses(mobileApplication, "Consulta");
            Paciente.Uses(landingPage, "Consulta");

            Psicologo.Uses(mobileApplication, "Consulta");
            Psicologo.Uses(landingPage, "Consulta");

            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(PaymentMethod, "", "");
            apiRest.Uses(CalendarContext, "", "");
            apiRest.Uses(ChatBotContext, "", "");
            apiRest.Uses(AccountContext, "", "");
            apiRest.Uses(MentalHealthContext, "", "");
            apiRest.Uses(GroupsContext, "", "");

            PaymentMethod.Uses(database, "", "");
            CalendarContext.Uses(database, "", "");
            ChatBotContext.Uses(database, "", "");
            AccountContext.Uses(database, "", "");
            MentalHealthContext.Uses(database, "", "");
            GroupsContext.Uses(database, "", "");

            PaymentMethod.Uses(Yape, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Plin, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Tunki, "API Request", "JSON/HTTPS");
            PaymentMethod.Uses(Visa, "API Request", "JSON/HTTPS");

            // Tags
            mobileApplication.AddTags("MobileApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");

            string contextTag = "Context";

            PaymentMethod.AddTags(contextTag);
            CalendarContext.AddTags(contextTag);
            ChatBotContext.AddTags(contextTag);
            AccountContext.AddTags(contextTag);
            MentalHealthContext.AddTags(contextTag);
            GroupsContext.AddTags(contextTag);

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
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
            componentView.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}
