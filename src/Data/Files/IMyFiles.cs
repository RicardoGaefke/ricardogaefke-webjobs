using RicardoGaefke.Domain;

namespace RicardoGaefke.Data
{
  public interface IMyFiles
  {
    Inserted Insert(Form data);
    Inserted GetFileInfo(int id);
    void UpdateFileInfo(Inserted data);
  }
}
