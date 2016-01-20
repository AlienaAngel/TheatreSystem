//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using TheatreSystem.Models;

//namespace TheatreSystem.Controllers
//{

//    public class GenController : Controller
//    {
//        private Context db = new Context();

//        [HttpPost]
//        public ActionResult PlaceGen()
//        {
//            foreach (var item in db.Zones.ToList())
//            {
//                int max = item.Places.Select(x => x.Number).Max();
//                for (int i = max; i < max+20; i++)
//                {
//                    Place p = new Place();
//                    p.Number = i;
//                    p.ZoneId = item.Id;

//                    db.Places.Add(p);
//                }
//            }

//            db.SaveChanges();

//            return View();
//        }

//        private void genOrder(User user, Play p, Place pc)
//        {
//            Order order = new Order();
//            order.UserId = user.Id;
//            db.Orders.Add(order);

//            Ticket ticket = new Ticket();
//            ticket.Serial = Guid.NewGuid().ToString();

//            //TODO
//            //ticket.Play = p;
//            ticket.Place = pc;

//            ticket.OrderId = order.Id;
//            db.Tickets.Add(ticket);
//            db.SaveChanges();
//        }




//        private Random r = new Random();

//        #region Генерация пьес и мест
//        private Place getPlace (Play p )
//        {

//            List<Ticket> tickets = db.Plays.Find(p.Id).Tickets;

//            Place place = null;

//            foreach (Zone z in db.Zones)
//            {
//                int count = z.Places.Count();
//                int ordered = tickets.Where(x => x.Place.ZoneId == z.Id).Count();
//                //    int count = db.Plays.Find(id).Tickets.Where(x => x.ZoneId == z.Id).Count();
//                //    int ordered = db.Plays.Find(id).Tickets.Where(x => x.Order != null && x.ZoneId == z.Id).Count();
//                count = count - ordered;

//                List<Place> or = tickets.Select(x => x.Place).Where(x => x.Zone.Id == z.Id).ToList();
//                List<Place> al = z.Places;

//                List<Place> sel = al.Union(or).Except(al.Intersect(or)).ToList();
//                if(sel.Count>0)
//                {
//                    place = sel.First();
//                    break;
//                }
                
//            }

//            return place;
//        }

//        private Play getPlay ()
//        {
//            Play p = db.Plays.OrderBy(r => Guid.NewGuid()).FirstOrDefault();
//            return p;
//        }

//        #endregion

//        [HttpPost]
//        public ActionResult GetOrders()
//        {
//            Place place = null;
//            Play play = null;
//            foreach (var user in db.Users.ToList()) 
//            {
//                switch(r.Next(4))
//                {
//                    case 0:
//                        break;
//                    case 1:
//                        for (int i = 0; i < 4; i++)
//                        {
//                            play = getPlay();
//                            place = getPlace(play);
//                            if (place != null && play != null)
//                            {
//                                genOrder(user, play, place);
//                            }
//                        }
//                        break;
//                    case 2:
//                        for (int i = 0; i < 3; i++)
//                        {
//                            play = getPlay();
//                            place = getPlace(play);
//                            if (place != null && play != null)
//                            {
//                                genOrder(user, play, place);
//                            }
//                        }
//                        break;
//                    case 3:
//                        for (int i = 0; i < 1; i++)
//                        {
//                            play = getPlay();
//                            place = getPlace(play);
//                            if (place != null && play != null)
//                            {
//                                genOrder(user, play, place);
//                            }
//                        }
//                        break;  
//                }    
//            }
            
//            return View();
//        }

//        [HttpPost]
//        public ActionResult GenPlays()
//        {
//            String baseInfo = "info about this play ";
//            String baseName = "Play about ";

//            for(int i = 0; i< 1000; i++)
//            {
//                Play p = new Play();
//                p.DatePlay = DateTime.Now.AddDays(i);
//                p.Info = baseInfo + i + "  " + Guid.NewGuid().ToString();
//                p.Name = baseName +i.GetHashCode();

//                db.Plays.Add(p);
//            }

//            db.SaveChanges();
//            return View();
//        }

//        [HttpPost]
//        public ActionResult GenUsers()
//        {

//            String baseUserName = "user";
//            String userPass = "123";
//            String emailPostfix = "@gmail.com";

//            for (int i = 0; i < 10000; i++)
//            {
//                TheatreSystem.Models.User user = new Models.User();
//                user.Name = baseUserName + i;
//                user.Email = user.Name + emailPostfix;
//                user.Password = userPass;

//                Role userRole = db.Roles.Where(x => x.Name.Equals(AuthorizationHelper.UserRole)).FirstOrDefault();
//                if(userRole!=null)
//                {
//                    user.Roles.Add(userRole);
//                }
//                db.Users.Add(user);

//            }

//            db.SaveChanges();

//            return View();
//        }
//    }
//}