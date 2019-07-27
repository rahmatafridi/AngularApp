import { Component, OnInit, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from '../../_models/photo';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../_services/auth.service';


@Component({
  selector: 'app-Photo-editor',
  templateUrl: './Photo-editor.component.html',
  styleUrls: ['./Photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() photos: Photo[];

 uploader: FileUploader ;
 hasBaseDropZoneOver: boolean= false;
baseUrl = environment.apiUrl;


  constructor(private authSeverice: AuthService) { }

  ngOnInit() {
this.initiazeUploader();
  }
   fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  initiazeUploader() {

    this.uploader = new FileUploader ({
     url: this.baseUrl + 'users/' + this.authSeverice.decodedToken.nameid + '/photos',
     /* authToken: 'Bearer' + localStorage.getItem('token'),
     isHTML5: true,
     allowedFileType: ['image'],
     removeAfterUpload: true,
     autoUpload: false,
     maxFileSize: 10 * 1024 * 1024 */
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
  /*   this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    }; */
  }
}
