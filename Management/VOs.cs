namespace Management
{
   
    /// <summary>
    /// Objeto para paginar entiedades
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="Items"></param>
    /// <param name="TotalItemCount"></param>
    /// <param name="CurentPage"></param>
    /// <param name="ItemsPerPage"></param>
    public record PaginatedListOutput<TEntity>(List<TEntity> Items, int TotalItemCount, int CurentPage, int ItemsPerPage);

   
}
