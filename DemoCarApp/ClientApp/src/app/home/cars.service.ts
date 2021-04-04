import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from '../model/cars';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  constructor(public http: HttpClient) { }

  getCars() {
    return this.http.get<Car[]>('cars');
  }

  addCar(car: Car) {
    return this.http.post<Car>('cars', car);
  }

  deleteCar(id: string, make: string) {
    return this.http.delete('cars/' + id + '/' + make);
  }

  getById(id: string, make: string) {
    return this.http.get<Car>('cars/' + id + '/' + make);
  }

  updateCar(id: string, car: Car) {
    return this.http.put('cars/' + id, car);
  }

}