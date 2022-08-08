using ESApp.Events;

namespace ESApp.Projections
{
    public class UserProjection : BaseProjection
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();

        public void Apply(AddPermission permission)
        {
            Permissions.Add(permission.description);
        }

        public void Apply(CreateUser createEvent)
        {
            FirstName = createEvent.FirstName;
            LastName = createEvent.LastName;
        }

        public void Apply(SetDepartment departmentEvent)
        {
            Department = departmentEvent.Department;
        }
    }
}