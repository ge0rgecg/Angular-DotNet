import { Router, ActivatedRoute } from '@angular/router';
import { LogService } from './../log.service';
import { Component, OnInit } from '@angular/core';
import { Log } from '../log.model';

@Component({
  selector: 'app-log-update',
  templateUrl: './log-update.component.html',
  styleUrls: ['./log-update.component.css']
})
export class LogUpdateComponent implements OnInit {

  log: Log;

  constructor(
    private LogService: LogService,
    private router: Router,
    private route: ActivatedRoute) {
      const id = this.route.snapshot.paramMap.get('id');
      this.LogService.readById(id).subscribe(log => {
        this.log = log;
      })
    }

  ngOnInit(): void {
  }

  updateLog(): void{
    this.LogService.update(this.log).subscribe(() => {
      this.LogService.showMessage('Log atualizado com sucesso!');
      this.router.navigate(['/logs']);
    })
  }

  cancel(): void {
    this.router.navigate(['/logs']);
  }
}
