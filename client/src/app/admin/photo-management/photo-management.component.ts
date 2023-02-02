import { Component, OnInit } from '@angular/core';
import {AdminService} from "../../_services/admin.service";
import {Photo} from "../../_models/photo";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {
  photos: Photo[] = [];

  constructor(private adminService: AdminService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getPhotos();
  }

  getPhotos() {
    this.adminService.getPhotosForApproval()
      .subscribe((photos) => {
        this.photos = photos;
        console.log(photos);
      });
  }

  approvePhoto(id: number) {
    this.adminService.approvePhoto(id)
      .subscribe(() => this.getPhotos());
  }

  rejectPhoto(id: number) {
    this.adminService.rejectPhoto(id)
      .subscribe(() => this.getPhotos());
  }

}
