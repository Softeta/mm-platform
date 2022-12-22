namespace Domain.Seedwork.Attributes
{
    /// <summary>
    /// There is projection field. It can be only sync but it cannot be changed through aggregate logic.
    /// </summary>
    public class ProjectionFieldAttribute : Attribute
    {
    }

}
