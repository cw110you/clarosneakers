using webstore.Models;

namespace webstore.Data
{
    public class InitialStates
    {
        public State[] states { get; set; }

        public InitialStates()
        {
            states = new State[]
            {
                new State{ Id = (int)StateIDs.Placed, Name ="訂單成立" },
                new State{ Id = (int)StateIDs.Processing, Name ="處理中" },
                new State{ Id = (int)StateIDs.Delivered, Name ="已出貨" },
                new State{ Id = (int)StateIDs.Completed, Name ="交易完成" },

                new State{ Id = (int)StateIDs.Canceled, Name ="訂單取消" }
            };
        }
    }
}