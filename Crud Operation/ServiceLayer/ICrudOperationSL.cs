using Crud_Operation.CommonLayer.Model;

namespace Crud_Operation.ServiceLayer
{
    public interface ICrudOperationSL
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
        public Task<ReadRecordResponse> ReadRecord();
    }

}
