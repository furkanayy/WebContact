using Business.Abstract.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BaseController : Controller
    {
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                    unitOfWork = (IUnitOfWork)HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
                return unitOfWork;
            }
        }
    }
}
