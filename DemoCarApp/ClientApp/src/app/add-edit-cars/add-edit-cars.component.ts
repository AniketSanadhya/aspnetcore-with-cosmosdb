import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CarsService } from '../home/cars.service';
import { Car } from '../model/cars';

@Component({
  selector: 'app-add-edit-cars',
  templateUrl: './add-edit-cars.component.html',
  styleUrls: ['./add-edit-cars.component.css']
})
export class AddEditCarsComponent implements OnInit {

  car: Car = <Car>{};
  isEdit = false;
  constructor(public carService: CarsService,
    public router: Router,
    public activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(response => {
      if (response['id']) {
        this.isEdit = true;
        this.getById(response['id'], response['make']);
      }
    })
  }

  ngOnInit() {
  }

  getById(id: string, make: string) {
    this.carService.getById(id,make).subscribe(response => {
      this.car = response;
    })
  }

  onSubmit() {
    if (!this.isEdit) {
      this.carService.addCar(this.car).subscribe(response => {
          this.router.navigate(['/']);
      })
    }
    else {
      this.carService.updateCar(this.car.id, this.car).subscribe(response => {
        // TODO: add error handling
        this.router.navigate(['/']);
      })
    }
  }
}
