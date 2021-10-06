﻿using System;
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
                    Console.WriteLine($"Searched room for id - {room.Id}:\n\tNumber: {room.RoomNumber}" +
                        $"\tCategory: {room.Category}\tPrice for one night: {room.Price}");
                else Console.WriteLine($"Searched room for id - {room.Id} wasn't found");
            }




            //ShowAllRooms(roomRepository);
            FindRoom(roomRepository);


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