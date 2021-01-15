import {LogService} from './../../log/log.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})

export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;
  fileToUpload: File = null;
  url = '';
  files: Array<any> = new Array<any>();
  
  constructor(private logService: LogService) { }

  ngOnInit() {
  }

  onSelectFile(files: FileList) {
    if (files.length === 0) {
        return;
    }
    this.fileToUpload = files.item(0);

    const fileReader: FileReader = new FileReader();
    fileReader.readAsDataURL(this.fileToUpload);

    fileReader.onload = (event: any) => {
        this.url = event.target.result;
    };

    this.files.push({ data: this.fileToUpload, fileName: this.fileToUpload.name });

    this.logService.uploadProfilePicture(this.files[0])
        .subscribe(() => {
          this.logService.showMessage('Arquivo enviado com sucesso!');
        });
  }
}