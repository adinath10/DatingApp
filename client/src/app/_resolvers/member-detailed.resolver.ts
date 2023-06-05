import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Injectable({
  providedIn: 'root'
})

// Use resolver when you want to fetch the data even before the user is routed to the URL.
// Resolver could include service calls which would bring us the data required to load the next page.

export class MemberDetailedResolver implements Resolve<Member> {
  constructor(private memberService: MembersService){}

  // ActivatedRoute allows us to access all resolved data and use it in our Component.
  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.memberService.getMember(route.paramMap.get('username')!);
  }
}
