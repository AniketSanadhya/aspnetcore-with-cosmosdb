import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Car } from '../model/cars';
import { CarsService } from './cars.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  cars: Car[] = []
  constructor(public carService: CarsService,
              public router: Router) {
    this.getCars();
  }

  ngOnInit(): void {
  }

  getCars() {
    this.carService.getCars().subscribe(response => {
      this.cars = response;
    })
  }

  onDelete(id: string, make: string) {
    this.carService.deleteCar(id,make).subscribe(response => {
      // TODO: add error handling
      this.getCars();
    })
  }
}
