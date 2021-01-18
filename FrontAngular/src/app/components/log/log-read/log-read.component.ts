import { LogService } from './../log.service';
import { Component, OnInit } from '@angular/core';
import { Log } from '../log.model';

@Component({
  selector: 'app-log-read',
  templateUrl: './log-read.component.html',
  styleUrls: ['./log-read.component.css']
})
export class LogReadComponent implements OnInit {

  logs: Log[];
  displayedColumns = ['id','chave','dataLog','texto','codigoRetorno','retorno','action'];
  constructor(private logService: LogService) { }

  ngOnInit(): void {
    this.logService.read().subscribe(logs => {
      this.logs = logs;
    })
  }

  delete(id:string):void{
    debugger;
    this.logService.delete(id).subscribe(() => {
      this.logService.showMessage('Log deletado com sucesso!');
    })
  }
}
