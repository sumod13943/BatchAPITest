using BatchAPI.BatchData;
using BatchAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchAPITest
{
    public class BatchDataMock : IBatchData
    {
        private readonly List<Batch> _batchList = new List<Batch>();

        public BatchDataMock() //Mocking
        {
            Attributes attribute = new Attributes();

            _batchList = new List<Batch>()
            {
                new Batch()
                {
                  BatchId = new Guid("9E13D973-8511-45DA-8217-95C961CC1DFB"), BusinessUnit="BU1", ExpiryDate=DateTime.Now.AddDays(1),
                  ACL= new ACL()
                    {
                        Id=1,
                        ReadGroups = new  List<string>(){ "ReadGroups1", "ReadGroups2" },
                        ReadUsers= new  List<string>(){ "ReadUsers1", "ReadUsers2" }
                    },
                    Attributes = new List<Attributes>()
                    {
                        new Attributes()
                        {
                            Id = 1,
                            Key = "Key1",
                            Value = "Value1"
                        },
                    }
                }
            };
        }
        public Batch AddBatch(Batch batch)
        {
            batch.BatchId = Guid.NewGuid();
            _batchList.Add(batch);

            return batch;
        }

        public List<Batch> GetBatch()
        {
            return _batchList;
        }

        public Batch GetBatch(Guid batchId)
        {
            return _batchList.SingleOrDefault(p => p.BatchId.Equals(batchId));
        }
    }
}
