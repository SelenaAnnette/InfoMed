﻿namespace doc_int
{
    using DataLayer.Persistence.AllergicReaction;
    using DataLayer.Persistence.Consultation;
    using DataLayer.Persistence.Disease;
    using DataLayer.Persistence.Group;
    using DataLayer.Persistence.Hospital;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Operation;
    using DataLayer.Persistence.Person;
    using DataLayer.Persistence.RiskFactor;
    using DataLayer.Persistence.Symptom;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Measuring;    

    using Ninject;

    public static class Binder
    {
        public static readonly IKernel NinjectKernel = new StandardKernel();

        static Binder()
        {
            ApplyBindings();
        }

        private static void ApplyBindings()
        {
            var mainDataBaseConnectionString = Properties.Settings.Default.MainDBConnectionString;
            var trashDataBaseConnectionString = Properties.Settings.Default.TrashDBConnectionString;

            NinjectKernel.Bind<IPersonRepository>().To<PersonRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IGroupRepository>().To<GroupRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<ISymptomRepository>().To<SymptomRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IRiskFactorRepository>().To<RiskFactorRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IMedicamentRepository>().To<MedicamentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IMeasuringTypeRepository>().To<MeasuringTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<ICredentialsRepository>().To<CredentialsRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IContactTypeRepository>().To<ContactTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IAssignedSymptomRepository>().To<AssignedSymptomRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IAssignedRiskFactorRepository>().To<AssignedRiskFactorRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IAssignedMedicamentRepository>().To<AssignedMedicamentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonPersonRepository>().To<PersonPersonRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonGroupRepository>().To<PersonGroupRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            NinjectKernel.Bind<IAllergicReactionRepository>().To<AllergicReactionRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonAllergicReactionRepository>().To<PersonAllergicReactionRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IDiseaseRepository>().To<DiseaseRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonDiseaseRepository>().To<PersonDiseaseRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IConsultationTypeRepository>().To<ConsultationTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonConsultationRepository>().To<PersonConsultationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IHospitalDepartmentRepository>().To<HospitalDepartmentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IHospitalRepository>().To<HospitalRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonHospitalizationRepository>().To<PersonHospitalizationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IOperationRepository>().To<OperationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            NinjectKernel.Bind<IPersonOperationRepository>().To<PersonOperationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            NinjectKernel.Bind<IMessageRepository>().To<MessageRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            NinjectKernel.Bind<INotificationRepository>().To<NotificationRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            NinjectKernel.Bind<IPersonMeasuringRepository>().To<PersonMeasuringRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            NinjectKernel.Bind<IPersonMedicamentRepository>().To<PersonMedicamentRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            NinjectKernel.Bind<IPersonRiskFactorRepository>().To<PersonRiskFactorRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            NinjectKernel.Bind<IPersonSymptomRepository>().To<PersonSymptomRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
        }
    }
}
