using MediatR.Pipeline;

namespace BudgetR.Server.Handlers.PipelineBehaviors;

public class PreRequestBehavior<TRequest> : IRequestPreProcessor<TRequest>
{
    //private ServerContainer _stateContainer;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly BudgetRDbContext _budgetRDbContext;
    //private readonly UserManager<ApplicationUser> _userManager;

    //public PreRequestBehavior(ServerContainer stateContainer, IHttpContextAccessor httpContextAccessor, BudgetRDbContext budgetRDbContext, UserManager<ApplicationUser> userManager)
    //{
    //    _stateContainer = stateContainer;
    //    _httpContextAccessor = httpContextAccessor;
    //    _budgetRDbContext = budgetRDbContext;
    //    _userManager = userManager;
    //}

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        //_stateContainer.ProcessName = GetHandlerName();
        //_stateContainer.BtaId = null;

        ////string email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
        //var userContext = _httpContextAccessor.HttpContext?.User;
        //ApplicationUser? appUser = await _userManager.GetUserAsync(userContext);

        //if (appUser != null)
        //{
        //    var user = _budgetRDbContext.Users
        //        .Where(x => x.AuthenticationId == appUser.Id)
        //        .Select(u => new User
        //        {
        //            UserId = u.UserId,
        //            HouseholdId = u.HouseholdId,
        //            UserType = u.UserType,
        //            IsActive = u.IsActive,
        //        })
        //        .FirstOrDefault();

        //    _stateContainer.ApplicationUserId = appUser.Id;
        //    _stateContainer.Email = appUser.Email;

        //    if (user != null)
        //    {
        //        _stateContainer.UserId = user.UserId;
        //        _stateContainer.HouseholdId = user.HouseholdId;
        //        _stateContainer.UserType = user.UserType;
        //        _stateContainer.IsActive = user.IsActive;
        //    }
        //}
    }

    protected string GetHandlerName()
    {
        string handlerName = typeof(TRequest).DeclaringType.Name;
        string folderName = typeof(TRequest).Namespace.Split(".").Last();

        return folderName + "." + handlerName;
    }
}
