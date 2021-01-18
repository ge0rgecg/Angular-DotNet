import { Router } from '@angular/router';
import { LogService } from './../log.service';
import { Component, OnInit } from '@angular/core';
import { Log } from '../log.model';

@Component({
  selector: 'app-log-create',
  templateUrl: './log-create.component.html',
  styleUrls: ['./log-create.component.css']
})
export class LogCreateComponent implements OnInit {

  log: Log = {
    id: 0,
    chave: "",
    dataLog: "",
    texto: "",
    codigoRetorno: 0,
    retorno: null
  }

  constructor(private LogService: LogService,
    private router: Router) { }

  ngOnInit(): void {
  }

  createLog(): void{
    debugger;
    this.LogService.create(this.log).subscribe(() => {
      debugger;
      this.LogService.showMessage('Log criado!');
      this.router.navigate(['/logs']);
    })
  }

  cancel(): void{
    this.router.navigate(['/logs']);
  }

}
