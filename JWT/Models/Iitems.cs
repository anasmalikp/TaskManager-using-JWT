namespace JWT.Models
{
    public interface Iitems
    {
        public void createtask(Items newitem);
        public List<Items> Alltask();
        public Items getbyid(int Id);
        public void updatetask(int Id, Items item);
        public void deletetask(int Id);
    }

    public class Itemservices : Iitems
    {

        List<Items> items = new List<Items>
        {
            new Items{Id  = 1, taskTitle="attend the review", Description = "pass the review", Status = "done"},
            new Items{Id = 2, taskTitle = "get into the project", Description = "help in the project", Status = "pending" }
        };
        public void createtask(Items newitem)
        {
            
            items.Add(newitem);
        }

        public Items getbyid(int Id)
        {
            return items.FirstOrDefault(x => x.Id == Id);
        }

        public List<Items> Alltask()
        {
            return items;
        }

        public void updatetask(int Id, Items item)
        {
            var toupdate = items.FirstOrDefault(x=> x.Id == Id);
            toupdate.taskTitle = item.taskTitle;
            toupdate.Description = item.Description;
            toupdate.Status = item.Status;
        }

        public void deletetask(int Id)
        {
            var todelete = items.FirstOrDefault(x => x.Id == Id);
            items.Remove(todelete);
        }
    }
}
