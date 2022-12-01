namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {

        //Veri tabanına yapılacak olan işlemleri tek bir transaction üzerinden gerçekleştirmeye yaramaktadır.
        //Örneğin iki yerden saveschanges özelliğini çağırmak yerine ikisini bitirip unitofwork üzerinden çağırırsın bu sayede birinde hata varsa o geri alınır işlem iptal edilir.
        Task CommitAsync();
        void Commit();
    }
}
