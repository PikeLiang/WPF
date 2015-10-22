
namespace SilverlightWCF_RIA.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the StudentsEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class DomainServiceRIA : LinqToEntitiesDomainService<StudentsEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'student' query.
        public IQueryable<student> GetStudent()
        {
            return this.ObjectContext.student;
        }

        public void InsertStudent(student student)
        {
            if ((student.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(student, EntityState.Added);
            }
            else
            {
                this.ObjectContext.student.AddObject(student);
            }
        }

        public void UpdateStudent(student currentstudent)
        {
            this.ObjectContext.student.AttachAsModified(currentstudent, this.ChangeSet.GetOriginal(currentstudent));
        }

        public void DeleteStudent(student student)
        {
            if ((student.EntityState == EntityState.Detached))
            {
                this.ObjectContext.student.Attach(student);
            }
            this.ObjectContext.student.DeleteObject(student);
        }
    }
}


