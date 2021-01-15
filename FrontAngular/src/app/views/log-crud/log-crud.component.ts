import { HeaderService } from './../../components/template/header/header.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-log-crud',
  templateUrl: './log-crud.component.html',
  styleUrls: ['./log-crud.component.css']
})
export class LogCrudComponent implements OnInit {

  constructor(
    private router: Router,
    private headerService: HeaderService) { 
      headerService.headerData = {
        title: 'Cadastro de logs',
        icon: 'storefront',
        routeUrl: '/logs'
      }
    }

  ngOnInit(): void {
  }

  navigateToLogCreate(): void{
    this.router.navigate(['/logs/create'])
  }

}