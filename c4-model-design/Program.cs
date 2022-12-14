using System;
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
            SoftwareSystem GoogleCalendar = model.AddSoftwareSystem("Google Calendar", "Permite agendar citas");
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
            styles.Add(new ElementStyle("GoogleMeet") { Background = "#3fb504", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleCalendar") { Background = "#3fb504", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleAccount") { Background = "#3fb504", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("ChatBot") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.RoundedBox });

            SystemContextView contextView = viewSet.CreateSystemContextView(Enlazador, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores
            Container mobileApplication = Enlazador.AddContainer("Mobile App", "Permite a los usuarios ver sus próximas citas, tratar sus problemas de salud mental, y editar sus datos", "Swift UI");
            Container landingPage = Enlazador.AddContainer("Landing Page", "", "React");
            Container apiRest = Enlazador.AddContainer("API REST", "API Rest", "NodeJS (NestJS) port 8080");

            Container PaymentContext = Enlazador.AddContainer("Payment Context", "Bounded Context de cobro y pago de la aplicación, para que los pacientes agenden citas", "NodeJS (NestJS)");
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

            apiRest.Uses(PaymentContext, "", "");
            apiRest.Uses(AppointmentContext, "", "");
            apiRest.Uses(ChatBotContext, "", "");
            apiRest.Uses(AccountContext, "", "");
            apiRest.Uses(MentalHealthContext, "", "");
            apiRest.Uses(GroupsContext, "", "");

            PaymentContext.Uses(database, "", "");
            AppointmentContext.Uses(database, "", "");
            ChatBotContext.Uses(database, "", "");
            AccountContext.Uses(database, "", "");
            MentalHealthContext.Uses(database, "", "");
            GroupsContext.Uses(database, "", "");

            PaymentContext.Uses(Yape, "API Request", "JSON/HTTPS");
            PaymentContext.Uses(Plin, "API Request", "JSON/HTTPS");
            PaymentContext.Uses(Tunki, "API Request", "JSON/HTTPS");
            PaymentContext.Uses(Visa, "API Request", "JSON/HTTPS");

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

            PaymentContext.AddTags(contextTag);
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
            
            // 3.1. Diagrama de Componentes (Account Context)
            Component accountController = AccountContext.AddComponent("Account Controller", "Controlador de Cuentas de Usuario.", "NodeJS (NestJS) REST Controller");
           
            Component accountCreator = AccountContext.AddComponent("Account Creator", "permite crear cuentas de usuario, pertenece a la capa Application de DDD", "NestJS Component");
            Component accountLogger = AccountContext.AddComponent("Account Logger", "permite validar los datos de login de un usuario, pertenece a la capa Application de DDD", "NestJS Component");
            
            Component accountRepository = AccountContext.AddComponent("Account Repository", "Concede el acceso a la base de datos de cuentas del usuario", "NestJS Component");
            Component accountMapper = AccountContext.AddComponent("Account Mapper", "Mapea los usuarios guardados en la base de datos", "NestJS Component");
            
            Component tempAccount = AccountContext.AddComponent("Temporal Logged Account", "Almacena los datos temporales del usuario en el que se ha iniciado sesión", "NestJS Component");
            Component facadeAccounts = AccountContext.AddComponent("Facade Account", "Sistema que permite registrarse o iniciar sesión con cuentas de otras plataformas", "NestJS Component");


            apiRest.Uses(accountController, "", "JSON/HTTPS");

            accountController.Uses(accountCreator, "Usa", "");
            accountController.Uses(accountLogger, "Usa", "");

            accountCreator.Uses(accountRepository, "Usa", "");
            accountCreator.Uses(facadeAccounts, "Usa", "");
            accountLogger.Uses(tempAccount, "Usa", "");
            accountLogger.Uses(accountRepository, "Usa", "");
            accountLogger.Uses(facadeAccounts, "Usa", "");
            accountRepository.Uses(accountMapper, "Solicita que lea o realice cambios a la base de datos", "");

            accountMapper.Uses(database, "Escribe y Lee datos de usuario", "");


            facadeAccounts.Uses(GoogleAccount, "Llama al API", "JSON/HTTPS");

            // Tags
            accountController.AddTags("Controller");
            accountCreator.AddTags("Service");
            accountLogger.AddTags("Service");
            accountRepository.AddTags("Repository");
            accountMapper.AddTags("Repository");
            tempAccount.AddTags("Entity");
            facadeAccounts.AddTags("Facade");
            AccountContext.AddTags("Account");



            styles.Add(new ElementStyle("Controller") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Service") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Account") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Repository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Entity") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("Facade") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewAccount = viewSet.CreateComponentView(AccountContext, "AccountComponents", "Component Diagram");
            componentViewAccount.PaperSize = PaperSize.A4_Landscape;
            componentViewAccount.Add(mobileApplication);
            componentViewAccount.Add(apiRest);
            componentViewAccount.Add(database);
            componentViewAccount.Add(GoogleAccount);

            componentViewAccount.AddAllComponents();
            // 3.2. Diagrama de Componentes (Chatbot Context)
            Component chatbotController = ChatBotContext.AddComponent("Chatbot Controller", "Controlador de Chatbot, pertenece a la capa Application de DDD", "NodeJS (NestJS) REST Controller");

            Component mentalHealthIllnessRepository = ChatBotContext.AddComponent("Mental Health Illness Repository", "Permite al chatbot leer la información de distintos tipos de enfermedades mentales", "NestJS Component") ?? throw new ArgumentNullException("ChatBotContext.AddComponent(\"Mental Health Illness Repository\", \"Permite al chatbot leer la información de distintos tipos de enfermedades mentales\", \"NestJS Component\")");
            Component mentalHealthIllness = ChatBotContext.AddComponent("Mental Health Illness", "Almacena la enfermedad mental que el chatbot ha detectado", "NestJS Component");
            
            Component chatFarcade = ChatBotContext.AddComponent("Facade Chatbot", "Permite que los usuarios interactuen con un chatbot real, y que este indique al sistema que accion desea realizar el usuario, o que información desea visualizar", "NestJS Component");


            apiRest.Uses(chatbotController, "", "JSON/HTTPS");

            chatbotController.Uses(chatFarcade, "Usa", "");
            chatbotController.Uses(mentalHealthIllness, "Usa", "");
            chatbotController.Uses(mentalHealthIllnessRepository, "Usa", "");



            mentalHealthIllnessRepository.Uses(database, "Lee información sobre enfermedades mentales de la base de datos", "");
            mentalHealthIllness.Uses(database, "Asocia un paciente a una enfermedad mental", "");


            chatFarcade.Uses(ChatBot, "Llama al API", "JSON/HTTPS");

            // Tags
            chatbotController.AddTags("ControllerChatbot");
            mentalHealthIllnessRepository.AddTags("MentalRepository");
            mentalHealthIllness.AddTags("MentalHealthIllness");
            chatFarcade.AddTags("FacadeChat");


            styles.Add(new ElementStyle("MentalHealthIllness") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FacadeChat") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("ControllerChatbot") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });;

            ComponentView componentViewChatbot = viewSet.CreateComponentView(ChatBotContext, "ChatbotComponents", "Component Diagram");
            componentViewChatbot.PaperSize = PaperSize.A4_Landscape;
            componentViewChatbot.Add(mobileApplication);
            componentViewChatbot.Add(apiRest);
            componentViewChatbot.Add(database);
            componentViewChatbot.Add(ChatBot);

            componentViewChatbot.AddAllComponents();


            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);

            // 3.3. Diagrama de Componentes (Payment Context)
            Component domainLayer = PaymentContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component paymentController = PaymentContext.AddComponent("Payment Controller", "REST API de pago de citas.", "NodeJS (NestJS) REST Controller");
            Component paymentApplicationService = PaymentContext.AddComponent("Dates PaymentService", "Provee métodos para el pago de citas, pertenece a la capa Application de DDD", "NestJS Component");
            Component paymentRepository = PaymentContext.AddComponent("Payment Repository", "Cuentas del usuario", "NestJS Component");
            Component paymentEntity = PaymentContext.AddComponent("Payment Entity", "Datos de los metodos de pago", "NestJS Component");
            Component facadePayment = PaymentContext.AddComponent("Facade Payment", "Sistema que maneja el pago para distintas plataformas", "NestJS Component");


            apiRest.Uses(paymentController, "", "JSON/HTTPS");
            paymentController.Uses(paymentApplicationService, "Invoca métodos de monitoreo");

            paymentApplicationService.Uses(domainLayer, "Usa", "");
            paymentApplicationService.Uses(paymentEntity, "Usa", "");
            paymentApplicationService.Uses(paymentRepository, "Usa", "");
            paymentApplicationService.Uses(facadePayment, "Usa", "");

            paymentRepository.Uses(database, "Usa", "");


            facadePayment.Uses(Yape, "Makes API calls to", "JSON/HTTPS");
            facadePayment.Uses(Plin, "Makes API calls to", "JSON/HTTPS");
            facadePayment.Uses(Tunki, "Makes API calls to", "JSON/HTTPS");
            facadePayment.Uses(Visa, "Makes API calls to", "JSON/HTTPS");

            // Tags
            domainLayer.AddTags("DomainLayer");
            paymentController.AddTags("PaymentController");
            paymentApplicationService.AddTags("PaymentApplicationService");
            paymentRepository.AddTags("PaymentRepository");
            paymentEntity.AddTags("PaymentEntity");
            facadePayment.AddTags("FacadePayment");

            styles.Add(new ElementStyle("DomainLayer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentDomainModel") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentEntity") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FacadePayment") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewPayment = viewSet.CreateComponentView(PaymentContext, "PaymentComponents", "Component Diagram");
            componentViewPayment.PaperSize = PaperSize.A4_Landscape;
            componentViewPayment.Add(mobileApplication);
            componentViewPayment.Add(apiRest);
            componentViewPayment.Add(database);
            componentViewPayment.Add(Visa);
            componentViewPayment.Add(Yape);
            componentViewPayment.Add(Tunki);
            componentViewPayment.Add(Plin);

            componentViewPayment.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);

            // 3.4. Diagrama de Componentes (Appointment Context)

            Component appointmentController = AppointmentContext.AddComponent("AppointmentController", "REST API endpoints de gestión de citas.", "NodeJS (NestJS) REST Controller");
            Component appointmentApplicationService = AppointmentContext.AddComponent("AppontmentApplicationService", "Provee métodos para la gestión de citas, pertenece a la capa Application de DDD", "NestJS Component");
            Component domainLayerAppointment = AppointmentContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component videoCallSystem = AppointmentContext.AddComponent("VideoCallSystem", "", "Component: NestJS Component");
            Component scheduleRepository = AppointmentContext.AddComponent("ScheduleRepository", "Horarios del psicólogo", "Component: NestJS Component");
            Component reviewRepository = AppointmentContext.AddComponent("ReviewRepository", "Comentarios a los psicológos", "Component: NestJS Component");
            Component appointmentDateRepository = AppointmentContext.AddComponent("AppointmentDateRepository", "Fechas de citas", "Component: NestJS Component");

            apiRest.Uses(appointmentController, "", "JSON/HTTPS");
            appointmentController.Uses(appointmentApplicationService, "Invoca métodos de gestión de citas");

            appointmentApplicationService.Uses(domainLayerAppointment, "Usa", "");
            appointmentApplicationService.Uses(videoCallSystem, "", "");
            appointmentApplicationService.Uses(scheduleRepository, "", "");
            appointmentApplicationService.Uses(reviewRepository, "", "");
            appointmentApplicationService.Uses(appointmentDateRepository, "", "");

            scheduleRepository.Uses(database, "", "");
            reviewRepository.Uses(database, "", "");
            appointmentDateRepository.Uses(database, "", "");

            videoCallSystem.Uses(GoogleMeet, "JSON/HTTPS");
            scheduleRepository.Uses(GoogleCalendar, "JSON/HTTPS");

            // Tags
            domainLayerAppointment.AddTags("DomainLayerAppointment");
            appointmentController.AddTags("AppointmentController");
            appointmentApplicationService.AddTags("AppointmentApplicationService");
            videoCallSystem.AddTags("VideoCallSystem");
            scheduleRepository.AddTags("SceduleRepository");
            reviewRepository.AddTags("ReviewRepository");
            appointmentDateRepository.AddTags("AppointmentDateRepository");

            styles.Add(new ElementStyle("DomainLayerAppointment") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AppointmentController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AppointmentApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("VideoCallSystem") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("SceduleRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("ReviewRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AppointmentDateRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentAppointmentView = viewSet.CreateComponentView(AppointmentContext, "AppointmentComponent", "Component Diagram");
            componentAppointmentView.PaperSize = PaperSize.A4_Landscape;

            componentAppointmentView.Add(mobileApplication);
            componentAppointmentView.Add(apiRest);
            componentAppointmentView.Add(database);
            componentAppointmentView.Add(GoogleMeet);
            componentAppointmentView.Add(GoogleCalendar);

            componentAppointmentView.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);

            //3.5. Diagrama de Componentes (Groups Context)
            Component domainLayerGroupsContext = GroupsContext.AddComponent("Domain Layer Groups", "", "NodeJS (NestJS)");
            Component mentalCareGroupsController = GroupsContext.AddComponent("Mental Care Groups Controller", "REST API de mental care groups.", "NodeJS (NestJS) REST Controller");
            Component mentalCareGroupsService = GroupsContext.AddComponent("Mental Care Groups Service", "Provee métodos para la realización de grupos de pacientes para su interacción, pertenece a la capa Application de DDD", "NestJS Component");
            Component mentalCareGroupsMeetingFacade = GroupsContext.AddComponent("Meeting System Facade", "Provee un sistema de tipo facade para reuniones en grupo", "NestJS Component");
            Component accountsRepository = GroupsContext.AddComponent("Accounts Repository", "Cuentas de usuarios de la aplicación", "NestJS Component");
            Component messagesRepository = GroupsContext.AddComponent("Messages Repository", "Mensajes de conversaciones entre los participantes de la aplicación", "NestJS Component");
            Component motivacionEmocionalRepository = GroupsContext.AddComponent("Motivación Emocial Repository", "Metodos de apoyo emocional a los pacientes", "NestJS Component");

            apiRest.Uses(mentalCareGroupsController, "", "JSON/HTTPS");
            mentalCareGroupsController.Uses(mentalCareGroupsService, "Invoca métodos de grupos de cuidado mental");
            mentalCareGroupsService.Uses(domainLayerGroupsContext, "Usa", "");
            mentalCareGroupsService.Uses(mentalCareGroupsMeetingFacade, "", "");
            mentalCareGroupsService.Uses(accountsRepository, "", "");
            mentalCareGroupsMeetingFacade.Uses(GoogleMeet, "", "JSON/HTTPS");
            accountsRepository.Uses(GoogleAccount, "", "JSON/HTTPS");
            accountsRepository.Uses(database, "", "");
            mentalCareGroupsService.Uses(messagesRepository, "", "");
            messagesRepository.Uses(database, "", "");
            motivacionEmocionalRepository.Uses(database, "", "");
            mentalCareGroupsService.Uses(motivacionEmocionalRepository, "", "");

            // Tags
            domainLayerGroupsContext.AddTags("DomainLayerGroups");
            mentalCareGroupsController.AddTags("MentalCareGroupsController");
            mentalCareGroupsService.AddTags("MentalCareGroupsService");
            mentalCareGroupsMeetingFacade.AddTags("MentalCareGroupsMeetingFacade");
            accountsRepository.AddTags("AccountsRepository");
            messagesRepository.AddTags("MessagesRepository");
            motivacionEmocionalRepository.AddTags("MotivacionEmocionalRepository");

            styles.Add(new ElementStyle("DomainLayerGroups") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalCareGroupsController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalCareGroupsService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalCareGroupsMeetingFacade") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AccountsRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MessagesRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MotivacionEmocionalRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });


            ComponentView componentViewGroup = viewSet.CreateComponentView(GroupsContext, "GroupComponent", "Component Diagram");
            componentViewGroup.PaperSize = PaperSize.A4_Landscape;
            componentViewGroup.Add(mobileApplication);
            componentViewGroup.Add(apiRest);
            componentViewGroup.Add(database);
            componentViewGroup.Add(GoogleAccount);
            componentViewGroup.Add(GoogleMeet);

            componentViewGroup.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);


            //3.6. Diagrama de Componentes (Mental Health Diagnostic Context)

            Component domainLayerDiagnosticContext = MentalHealthContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component mentalCareDiagnosticController = MentalHealthContext.AddComponent("Mental Care Diagnostic Controller", "REST API de mental care groups.", "NodeJS (NestJS) REST Controller");
            Component mentalCareDiagnosticService = MentalHealthContext.AddComponent("Mental Care Diagnostic Service", "Provee métodos para la realización de grupos de pacientes para su interacción, pertenece a la capa Application de DDD", "NestJS Component");
            Component diagnosticRepository = MentalHealthContext.AddComponent("Accounts Repository", "Cuentas de usuarios de la aplicación", "NestJS Component");

            apiRest.Uses(mentalCareDiagnosticController, "", "JSON/HTTPS");

            mentalCareDiagnosticController.Uses(mentalCareDiagnosticService, "Invoca métodos para el control de diagnósticos");

            mentalCareDiagnosticService.Uses(domainLayerDiagnosticContext, "Usa", "");
            mentalCareDiagnosticService.Uses(ChatBot, "Usa", "");
            mentalCareDiagnosticService.Uses(diagnosticRepository, "", "");
            diagnosticRepository.Uses(database, "", "");
            mentalCareDiagnosticService.Uses(diagnosticRepository, "", "");
            ChatBot.Uses(database, "", "");


            //// Tags
            domainLayerDiagnosticContext.AddTags("DomainLayerDiagnostic");
            mentalCareDiagnosticController.AddTags("MentalCareDiagnosticController");
            mentalCareDiagnosticService.AddTags("MentalCareDiagnosticService");
            diagnosticRepository.AddTags("DiagnosticRepository");

            styles.Add(new ElementStyle("DomainLayerDiagnostic") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalCareDiagnosticController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MentalCareDiagnosticService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("DiagnosticRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewDiagnostic = viewSet.CreateComponentView(MentalHealthContext, "MentalDiagnosticComponent", "Component Diagram");
            componentViewDiagnostic.PaperSize = PaperSize.A4_Landscape;
            componentViewDiagnostic.Add(mobileApplication);
            componentViewDiagnostic.Add(apiRest);
            componentViewDiagnostic.Add(database);
            componentViewDiagnostic.Add(ChatBot);

            componentViewDiagnostic.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}