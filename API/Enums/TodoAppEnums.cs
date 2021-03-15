using System.ComponentModel;

namespace API.Enums
{
    public class TodoAppEnums
    {
        public enum TaskStatus{
            [Description("inprogress")]
            InProgress = 0,
            [Description("ontime")]
            OnTime = 1,
            [Description("late")]
            Late = 2
        }
    }
}