import {trigger, transition, style, query, group, animateChild, animate, keyframes, state} from '@angular/animations';

export const fader =
  trigger('routeAnimations', [
   state('void', style({
    opacity: 0
  })),
  transition('void <=> *', animate(1000)),
]);

export const joystickAnimation =
trigger('joystickTrigger', [
  state('isSignIn', style({transform: 'translateX(-100%)'}) ),
  state('isSignUp', style({transform: 'translateX(0%)'}) ),
  transition('* => isSignIn', translateJoystickTo('left') ),
  transition('* => isSignUp', translateJoystickTo('right') ),
  transition('isSignUp => *', translateJoystickTo('left') ),
  transition('isSignIn => *', translateJoystickTo('right') )
]) ;

export const panelAnimation =
trigger('panelTrigger', [
  state('isSignIn', style({transform: 'scaleX(2)'}) ),
  state('isSignUp', style({transform: 'scaleX(1)'}) ),
  transition('* => isSignIn', translatePanelTo('left') ),
  transition('* => isSignUp', translatePanelTo('right') ),
  transition('isSignUp => *', translatePanelTo('left') ),
  transition('isSignIn => *', translatePanelTo('right') )
]) ;

export const logoAnimation = trigger('logoTrigger', [
  state('isSignIn', style({
    transform: 'translateY(150%)'
  }) ),
  state('isSignUp', style({transform: 'translateY(0%) '}) ),
  transition('* => isSignIn', translateLogoTo('middle') ),
  transition('* => isSignUp', translateLogoTo('top') ),
  transition('isSignUp => *', translateLogoTo('middle') ),
  transition('isSignIn => *', translateLogoTo('top') )
]);

function translateJoystickTo(direction) {
  const optional = { optional: true };
  if (direction === 'left') {
    return [
      query(':self', [
        style({
          transform: 'translateX(0%) ',
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({ transform: 'translateX(-100%)'}))
        ], optional)
    ];
  } else if (direction === 'right') {
    return [
      query(':self', [
        style({
          transform: 'translateX(-100%)',
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({  transform: 'translateX(0%)'}))
        ], optional)
    ];
  }
}
function translatePanelTo(direction) {
  const optional = { optional: true };
  if (direction === 'right') {
    return [
      query(':self', [
        style({
          transform: 'scale(2)'
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({ transform: 'scaleX(1)'}))
        ], optional)
    ];
  } else if (direction === 'left') {
    return [
      query(':self', [
        style({
          transform: 'scaleX(1)',
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({  transform: 'scaleX(2)'}))
        ], optional)
    ];
  }
}
function translateLogoTo(direction) {

  const optional = { optional: true };
  if (direction === 'top') {
    return [
      query(':self', [
        style({
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({ transform: 'translateY(0%)'}))
        ], optional)
    ];
  } else if (direction === 'middle') {
    return [
      query(':self', [
        style({
        })
      ], optional),
        query(':self', [
          animate('600ms ease', style({  transform: 'translateY(150%)'}))
        ], optional)
    ];
  }
}
