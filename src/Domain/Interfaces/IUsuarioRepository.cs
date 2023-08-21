namespace Domain.Interfaces;

public interface IUsuarioRepository
{
    #region Login Methods
    Task<Usuario> GetByLogin(string login);
    #endregion
}