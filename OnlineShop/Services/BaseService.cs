using AuthorizationApp.Repositories;

namespace OnlineShop.Services
{
    public class BaseService
    {
        #region Protected Fields

        protected readonly UnitOfWork _unitOfWork;
        protected IAuthorizationService _authService;
        #endregion

        #region Constructors
        public BaseService(UnitOfWork unitOfWork, IAuthorizationService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        #endregion
    }
}
