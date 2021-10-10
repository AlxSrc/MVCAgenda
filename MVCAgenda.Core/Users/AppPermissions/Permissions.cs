using System.Collections.Generic;

namespace MVCAgenda.Core.Users.AppPermissions
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Patients
        {
            public const string View = "Permissions.Patients.View";
            public const string Create = "Permissions.Patients.Create";
            public const string Edit = "Permissions.Patients.Edit";
            public const string Delete = "Permissions.Patients.Delete";
        }

        public static class Appointments
        {
            public const string View = "Permissions.Appointments.View";
            public const string Create = "Permissions.Appointments.Create";
            public const string Edit = "Permissions.Appointments.Edit";
            public const string Delete = "Permissions.Appointments.Delete";
        }

        public static class Consultations
        {
            public const string View = "Permissions.Consultations.View";
            public const string Create = "Permissions.Consultations.Create";
            public const string Edit = "Permissions.Consultations.Edit";
            public const string Delete = "Permissions.Consultations.Delete";
        }

        public static class PatientSheets
        {
            public const string View = "Permissions.PatientSheets.View";
            public const string Create = "Permissions.PatientSheets.Create";
            public const string Edit = "Permissions.PatientSheets.Edit";
            public const string Delete = "Permissions.PatientSheets.Delete";
        }

        public static class Rooms
        {
            public const string View = "Permissions.Rooms.View";
            public const string Create = "Permissions.Rooms.Create";
            public const string Edit = "Permissions.Rooms.Edit";
            public const string Delete = "Permissions.Rooms.Delete";
        }

        public static class Medics
        {
            public const string View = "Permissions.Medics.View";
            public const string Create = "Permissions.Medics.Create";
            public const string Edit = "Permissions.Medics.Edit";
            public const string Delete = "Permissions.Medics.Delete";
        }
    }
}