using System;
using task2.Entity;
using task2.Repository;
using System.Collections.Generic;

namespace task2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //initialization
            Room testRoom1 = new Room(1, 1, "Lux", 120m);
            Room testRoom3 = new Room(3, 3, "Default", 30m);
            Room testRoom2 = new Room(2, 2, "Lux", 120m);

            Client testClient1 = new Client(1, "Alexsey Alex A", DateTime.Parse("5/1/2008"), "NK299138198");
            Client testClient2 = new Client(2, "Vasyanich Vasya V", DateTime.Parse("13/6/2001"), "NK2995438198");
            Client testClient3 = new Client(3, "Daniel Danya A", DateTime.Parse("2/2/1998"), "KB299138398");

            Booking testBooking1 = new Booking(1, 1, 1, DateTime.Parse("10/10/2021"), DateTime.Parse("15/10/2021"));
            Booking testBooking2 = new Booking(2, 1, 1, DateTime.Parse("20/10/2021"), DateTime.Parse("23/10/2021"), DateTime.Parse("1/10/2021"));
            Booking testBooking3 = new Booking(3, 3, 2, DateTime.Parse("8/11/2021"), DateTime.Parse("25/11/2021"));

            RoomRepository roomRepository = new RoomRepository("rooms.txt");
            ClientRepository clientRepository = new ClientRepository("clients.txt");
            BookingRepository bookingRepository = new BookingRepository("orders.txt");


            //methods
            void ShowAllRooms(RoomRepository roomRepository)
            {
                List<Room> _buff = new List<Room>();
                _buff.AddRange(roomRepository.GetAll());

                Console.WriteLine("-------------------------All Rooms-----------------------");
                foreach (var room in _buff)
                {
                    Console.WriteLine($"ID: {room.Id}\tNumber: {room.RoomNumber}\tCategory: {room.Category}\tPrice for one night: {room.Price}");
                }
                Console.WriteLine("---------------------------------------------------------");

            }

            void FindRoom(RoomRepository roomRepository)
            {
                ShowAllRooms(roomRepository);
                int number;
                Console.WriteLine("Enter id of the room:");
                Int32.TryParse(Console.ReadLine(), out number);

                Room room = roomRepository.Seek(number);
                if(room.Id != -1)
                    Console.WriteLine($"Searched room for id - {number}:\n\tNumber: {room.RoomNumber}" +
                        $"\tCategory: {room.Category}\tPrice for one night: {room.Price}");
                else Console.WriteLine($"Searched room for id - {number} wasn't found");
            }

            void DeleteRoom(RoomRepository roomRepository)
            {
                ShowAllRooms(roomRepository);
                Console.WriteLine("Enter id of the room ur want to delete:");
                Int32.TryParse(Console.ReadLine(), out int number);
                bool _isDeleted = roomRepository.Delete(number);
                if (_isDeleted)
                {
                    Console.WriteLine($"Room №{number} was successfully deleted ");
                }
                else Console.WriteLine("An error has occurred ");
            }
            //////////////////////////////////////////////////////////
            void ShowAllClients(ClientRepository clientRepository)
            {
                List<Client> _buff = new List<Client>();
                _buff.AddRange(clientRepository.GetAll());

                Console.WriteLine("-------------------------All Clients-----------------------");
                foreach (var client in _buff)
                {
                    Console.WriteLine($"ID: {client.Id}\tName: {client.Name}\tBirthday: {client.Birthday} \tPassport: {client.Passport}");
                }
                Console.WriteLine("---------------------------------------------------------");

            }

            void FindClient(ClientRepository clientRepository)
            {
                ShowAllClients(clientRepository);
                Console.WriteLine("Enter id of the client:");
                Int32.TryParse(Console.ReadLine(), out int number);

                Client client = clientRepository.Seek(number);
                if (client.Id != -1)
                    Console.WriteLine($"Searched client for ID: { client.Id}\tName: {client.Name}\tBirthday: { client.Birthday} \tPassport: { client.Passport}");
                else Console.WriteLine($"Searched client for id - {client.Id} wasn't found");
            }

            void DeleteClient(ClientRepository clientRepository)
            {
                ShowAllClients(clientRepository);
                Console.WriteLine("Enter id of the client ur want to delete:");
                Int32.TryParse(Console.ReadLine(), out int number);
                bool _isDeleted = clientRepository.Delete(number);
                if (_isDeleted)
                {
                    Console.WriteLine($"Client №{number} was successfully deleted ");
                }
                else Console.WriteLine("An error has occurred ");
            }

            void ShowAllBookings(BookingRepository bookingRepository)
            {
                List<Booking> _buff = new List<Booking>();
                _buff.AddRange(bookingRepository.GetAll());
                Console.WriteLine("-------------------------All Bookings-----------------------");
                foreach (var booking in _buff)
                {
                    Console.WriteLine($"ID: {booking.Id}\tClientID: {booking.ClientId}\tRoomID: {booking.RoomId} \tCheckIn: {booking.CheckIn} \tCheckOut: {booking.CheckOut} \tBookingDate: {booking.BookingDate}");
                }
                Console.WriteLine("---------------------------------------------------------");
            }
            
            void FindBooking(BookingRepository bookingRepository)
            {
                
                ShowAllBookings(bookingRepository);
                Console.WriteLine("Enter id of the booking:");
                Int32.TryParse(Console.ReadLine(), out int number);

                Booking booking = bookingRepository.Seek(number);
                if (booking.Id != -1)
                    Console.WriteLine($"Searched booking for ID: {number}\tClientID: {booking.ClientId}\tRoomID: {booking.RoomId} \tCheckIn: {booking.CheckIn} \tCheckOut{booking.CheckOut} \tBookingDate{booking.BookingDate}");
                else Console.WriteLine($"Searched booking for id - {number} wasn't found");
            }
            void DeleteBooking(BookingRepository bookingRepository)
            {
                ShowAllBookings(bookingRepository);
                Console.WriteLine("Enter id of the booking u want to delete:");
                Int32.TryParse(Console.ReadLine(), out int number);
                bool _isDeleted = bookingRepository.Delete(number);
                if (_isDeleted)
                {
                    Console.WriteLine($"Client №{number} was successfully deleted ");
                }
                else Console.WriteLine("An error has occurred ");
            }

            void FindRoomForDate()
            {
                Console.WriteLine("Do you want to stay for a day or for a period?\n1 - For a day\n2 - For a period");
                Int32.TryParse(Console.ReadLine(),out int n);
                if (n != 1 && n != 2)
                    Console.WriteLine("Wrong Number!");
                else if (n == 2)
                    FindRoomForPeriod();
                else FindRoomForDay();
            }
           
            void FindRoomForDay()
            {
                Console.WriteLine("Enter check in date\nin format dd/mm/yyyy");
                DateTime.TryParse(Console.ReadLine(), out DateTime CheckIn);

                List<Room> rooms = bookingRepository.FindRoomForDate(CheckIn, roomRepository);
                Console.WriteLine($"Here is list of free rooms for {CheckIn}:");
                foreach (var room in rooms)
                {
                    Console.Write($"{room.Id}|\t{room.RoomNumber}|\t{room.Category}|\t{room.Price}");
                }
                Console.WriteLine("\n");
            }

            void FindRoomForPeriod()
            {
                Console.WriteLine("Enter check in date\nin format dd/mm/yyyy");
                DateTime.TryParse(Console.ReadLine(), out DateTime CheckIn);

                Console.WriteLine("Enter check out date\nin format dd/mm/yyyy");
                DateTime.TryParse(Console.ReadLine(), out DateTime CheckOut);

                List<Room> rooms = bookingRepository.FindRoomForDate(CheckIn, CheckOut, roomRepository);
                Console.WriteLine($"Here is list of free rooms for {CheckIn} to {CheckOut}:");
                foreach (var room in rooms)
                {
                    Console.Write($"id: {room.Id}|\troom number: {room.RoomNumber}|\tcategory: {room.Category}|\tprice: {room.Price}");
                }
                Console.WriteLine("\n");
            }
            /////////////////////////////////////////////////////////

            //main
            //ShowAllRooms(roomRepository);
            //FindRoom(roomRepository);
            FindRoomForDate();








































            /*            roomRepository.Add(testRoom);
                        roomRepository.Add(testRoom2);
                        *//*roomRepository.Add(testRoom3);
                        roomRepository.GetAll();
                        //roomRepository.Seek(2);
                        roomRepository.Delete(3);*/


            /*                        
                        *//*            clientRepository.Add(testClient1);
                                    clientRepository.Add(testClient2);
                                    clientRepository.Add(testClient3);*//*
                                    clientRepository.GetAll();
                                    clientRepository.Seek(3);
                                    clientRepository.GetAll();
                                    //clientRepository.Delete(1);*/


            ;
            /*            bookingRepository.Add(testBooking3);
                        bookingRepository.Add(testBooking2);
                        bookingRepository.Add(testBooking1);

                        bookingRepository.Delete(1);

                        bookingRepository.Add(testBooking1);

                        bookingRepository.MakeBooking(4, testClient2, testRoom3, DateTime.Parse("10/10/2021"), DateTime.Parse("15/10/2021")); // should be done

                        bookingRepository.MakeBooking(4, testClient2, testRoom1, DateTime.Parse("10/10/2022"), DateTime.Parse("15/10/2022")); // shouldn't be done

                        bookingRepository.MakeBooking(5, testClient3, testRoom1, DateTime.Parse("16/10/2021"), DateTime.Parse("19/10/2021")); // should be done
                        bookingRepository.MakeBooking(6, testClient1, testRoom3, DateTime.Parse("8/11/2021"), DateTime.Parse("25/11/2021")); // should be done

                        bookingRepository.MakeBooking(7, testClient2, testRoom2, DateTime.Parse("10/11/2021"), DateTime.Parse("30/11/2021")); // shouldn't be done
                        bookingRepository.MakeBooking(7, testClient2, testRoom2, DateTime.Parse("1/11/2021"), DateTime.Parse("20/11/2021")); // shouldn't be done
                        bookingRepository.MakeBooking(7, testClient2, testRoom2, DateTime.Parse("1/11/2021"), DateTime.Parse("30/11/2021")); // shouldn't be done*/

            bookingRepository.FindRoomForDate(DateTime.Parse("15/10/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("10/11/2021"), roomRepository);

            bookingRepository.FindRoomForDate(DateTime.Parse("10/11/2021"), DateTime.Parse("30/11/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("1/11/2021"), DateTime.Parse("20/11/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("1/11/2021"), DateTime.Parse("30/11/2021"), roomRepository);

            bookingRepository.FindRoomForDate(DateTime.Parse("16/10/2021"), DateTime.Parse("19/10/2021"), roomRepository);

        }
    }
}
