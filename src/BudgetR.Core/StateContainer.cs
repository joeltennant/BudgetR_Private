using BudgetR.Core.Models;

namespace BudgetR.Core;
public class StateContainer
{
    private long? _currentUserId;
    private UserType? _userType;
    private long? _householdId;
    private string? _processName;
    private long? _btaId;
    private bool _isActive;
    private string? firstName;
    private string? email;

    public string? Email { get; set; }

    public string? ApplicationUserId { get; set; }

    public long? UserId { get; set; }

    public bool IsActive { get; set; }

    public UserType? UserType { get; set; }

    public long? HouseholdId { get; set; }

    public string? ProcessName { get; set; }

    public long? BtaId { get; set; }

    private MonthSelection? _monthSelection;

    public MonthSelection? MonthSelection
    {
        get => _monthSelection;
        set
        {
            _monthSelection = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
