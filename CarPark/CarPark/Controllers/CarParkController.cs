using CarPark.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarPark.Controllers
{

    [Route("api/[controller]")] //controller is sentemail
    [ApiController]
    public class CarParkController : Controller
    {
        private readonly AppDbContext _context;

        public CarParkController(AppDbContext context)
        {
            _context = context;
        }

        static bool carParkIsOpen = false;

        [HttpGet] //to open the park lot
        [Route("open")]
        public async Task<IActionResult> CarParkIsOpen()
        {
            carParkIsOpen=true;
            await _context.SaveChangesAsync();
            return Ok("Parking lot is open!");
        }

        [HttpGet] //to close the park lot
        [Route("close")]
        public async Task<IActionResult> CarParkIsClose()
        {
            carParkIsOpen=false;
            await _context.SaveChangesAsync();
            return Ok("Parking lot is closed!");
        }


        [HttpGet] //get method to get the list of all vehicles.
        public async Task<IActionResult> GetAllVehiclesAsync()
        {
            var allVehicles = await _context.Vehicles.ToListAsync();
            await _context.SaveChangesAsync();

            return Ok(allVehicles);
        }

        

        [HttpGet] //get method to get the list of class 1 vehicles.
        [Route("class1")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _context.Vehicles.Where(x => x.Class==1).ToListAsync();
            await _context.SaveChangesAsync();

            return Ok(vehicles);
        }

        [HttpGet] //get method to get the list of class 2 vehicles.
        [Route("class2")]
        public async Task<IActionResult> GetAllVehicles2()
        {
            var vehicles = await _context.Vehicles.Where(x => x.Class==2).ToListAsync();
            await _context.SaveChangesAsync();

            return Ok(vehicles);
        }

        [HttpGet] //get method to get the list of class 3 vehicles.
        [Route("class3")]
        public async Task<IActionResult> GetAllVehicle3s()
        {
            var vehicles = await _context.Vehicles.Where(x => x.Class==3).ToListAsync();
            await _context.SaveChangesAsync();

            return Ok(vehicles);
        }


        [HttpGet]
        [Route("hourse-power/{id}")]
        public async Task<IActionResult> GetBeygir(int id)
        {

            var existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(e => e.Id == id);
            if (existingVehicle == null)
            {
                return StatusCode(404, $"The car with {id} id is not found");
            }
            else
            {
                var hoursePower = existingVehicle.Kilowatt*1.341;
                return StatusCode(200, $" The horse power of the vehicle with id {id} is {hoursePower} HP. ");
            }
        }


        [HttpPost] //post method to add a vehicle
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle car)
        {
           string platePattern = @"^\d{2}\s[A-Z]{1,3}\s\d{2,4}$";
           string modelYearPattern = @"^\d{4}$";

            if (carParkIsOpen==true)
            {

                if (car.Class==1)
                {
                    if (Regex.IsMatch(car.LicensePlate, platePattern)==false)
                        return StatusCode(404, "License plate is not regular form!");

                    if (Regex.IsMatch(Convert.ToString(car.ModelYear), modelYearPattern)==false)
                        return StatusCode(404, "Model year must be a 4-digit number!");

                    if (car.LuggageCappacity!=null || car.SpareTyre==true)
                        return StatusCode(404, "Luggage Capacity must be null and spare tyre property must not be true for 1. class vehicles");

                    if (car.Price<=0 || car.AutoPilot!=true)
                        return StatusCode(404, "Price must be more than 0 and autopilot property must be true for 1. class vehicles");

                    if (car.CarWash==null)
                        return StatusCode(404, "CarWash property cant be null for 1. class vehicles");

                    if (car.TireChange==true)
                        return StatusCode(404, "1. Class vehicles cant use the tire changing service!");

                    if (car.Kilowatt<=0)
                        return StatusCode(404, "Kilowatt value of the vehicle must be more than 0.");


                    car.EntryTime = DateTime.Now;
                    await _context.Vehicles.AddAsync(car);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("AddVehicle", new { id = car.Id }, car);
                }

                if (car.Class==2)
                {
                    if (Regex.IsMatch(car.LicensePlate, platePattern)==false)
                        return StatusCode(404, "License plate is not regular form!");

                    if (Regex.IsMatch(Convert.ToString(car.ModelYear), modelYearPattern)==false)
                        return StatusCode(404, "Model year must be a 4-digit number!");

                    if (car.Price!=null || car.AutoPilot==true)
                        return StatusCode(404, "Price must be null and autopilot property must not be true for 2. class vehicles.");

                    if (car.SpareTyre!=true || car.LuggageCappacity<=0)
                        return StatusCode(404, "SpareTyre property must be true and luggage capacity must be more than 0 for 2. class vehicles.");

                    if (car.CarWash==true)
                        return StatusCode(404, "2. Class vehicles cant use the car washing service.");

                    if (car.TireChange==null)
                        return StatusCode(404, "Tire changing property cant be null for 2. class vehicles.");

                    if (car.Kilowatt<=0)
                        return StatusCode(404, "Kilowatt value of the vehicle must be more than 0.");

                    car.EntryTime = DateTime.Now;

                    await _context.Vehicles.AddAsync(car);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("AddVehicle", new { id = car.Id }, car);
                }

                if (car.Class==3)
                {
                    if (Regex.IsMatch(car.LicensePlate, platePattern)==false)
                        return StatusCode(404, "License plate is not regular form.");

                    if (Regex.IsMatch(Convert.ToString(car.ModelYear), modelYearPattern)==false)
                        return StatusCode(404, "Model year must be a 4-digit number.");

                    if (car.LuggageCappacity!=null || car.SpareTyre==true)
                        return StatusCode(404, "Luggage Capacity must be null, spare tyres must not be true for 3. class vehicles.");

                    if (car.Price!=null || car.AutoPilot==true)
                        return StatusCode(404, "Price must be null and autopilot property must not be true for 3. class vehicles.");

                    if (car.CarWash==true)
                        return StatusCode(404, "3. Class vehicles cant use the car washing service.");


                    if (car.TireChange==true)
                        return StatusCode(404, "3. Class vehicles cant use the tire changing service.");

                    if (car.Kilowatt<=0)
                        return StatusCode(404, "Kilowatt value of the vehicle must be more than 0.");

                    car.EntryTime = DateTime.Now;
                    await _context.Vehicles.AddAsync(car);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("AddVehicle", new { id = car.Id }, car);
                }

                return StatusCode(404, "Class of the vehicle must be 1, 2 or 3");
            }

            return StatusCode(404, "Parking lot is closed!");

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveVehicle(int id)
        {
            if (carParkIsOpen==true)
            {


                var vehicle = await _context.Vehicles.FindAsync(id);

                if (vehicle == null)
                    return StatusCode(404, $"The vehicle with id {id} has not found!");


                vehicle.ExitTime = DateTime.Now;

                var entryTime = vehicle.EntryTime.Value;
                var exitTime = vehicle.ExitTime.Value;

                var parkingDuration = exitTime - entryTime;

                var parkingFee = Convert.ToDouble(parkingDuration.TotalHours) * Convert.ToDouble("10");


                if (vehicle.Class==1)
                {
                    if (vehicle.CarWash == false)
                    {
                        parkingFee=3*parkingFee;
                        _context.Vehicles.Remove(vehicle);
                        await _context.SaveChangesAsync();

                        return StatusCode(200, $"The vehicle remained in the parking lot for {parkingDuration.TotalHours} hours. The fee is {parkingFee} TL");
                    }

                    //carWash==true

                    double carWashFee = 200.00;
                    parkingFee=3*parkingFee+ carWashFee;

                    _context.Vehicles.Remove(vehicle);
                    await _context.SaveChangesAsync();

                    return StatusCode(200, $"The vehicle remained in the parking lot for {parkingDuration.TotalHours} hours. The fee is {parkingFee} TL");
                }

                if (vehicle.Class==2)
                {
                    if (vehicle.TireChange==false)
                    {
                        parkingFee=2*parkingFee;
                        _context.Vehicles.Remove(vehicle);
                        await _context.SaveChangesAsync();

                        return StatusCode(200, $"The vehicle remained in the parking lot for {parkingDuration.TotalHours} hours. The fee is {parkingFee} TL");
                    }

                    //tireChange==true
                    double tireChangeFee = 200.00;
                    parkingFee=2*parkingFee+ tireChangeFee;

                    _context.Vehicles.Remove(vehicle);
                    await _context.SaveChangesAsync();

                    return StatusCode(200, $"The vehicle remained in the parking lot for {parkingDuration.TotalHours} hours. The fee is {parkingFee} TL");

                }

                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();

                return StatusCode(200, $"The vehicle remained in the parking lot for {parkingDuration.TotalHours} hours. The fee is {parkingFee} TL");
            }

            return StatusCode(404, "Parking lot is closed!");
        }

    }

}

