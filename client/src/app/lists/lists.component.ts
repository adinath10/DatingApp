import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  // Partial type is useful when working with APIs that return objects with a lot of properties, 
  // but not all of them are required.

  // When updating an object, we provide some of the properties. 
  // The Partial type makes it easy to create a type that represents the updated object, 
  // while still keeping the original type intact.
  members ?: Partial<Member[]>;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination ?: Pagination;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadLikes()
  }

  loadLikes(){
    this.memberService.getLikes(this.predicate ,this.pageNumber, this.pageSize).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadLikes();
  }

}
