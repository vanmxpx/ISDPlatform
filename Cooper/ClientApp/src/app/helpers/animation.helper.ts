import { trigger, transition, style, animate, query, stagger, animateChild, AnimationTriggerMetadata } from '@angular/animations';

export class AnimationsHelper {

    public static readonly listAnimation: AnimationTriggerMetadata[] = [
        trigger('item', [
            transition(':enter', [
                style({ transform: 'scale(0.5)', opacity: 0 }),
                animate('1s cubic-bezier(.8, -0.6, 0.5, 1.5)',
                style({
                    transform: 'scale(1)', opacity: 1
                }))
            ])
        ]),
        trigger('list', [
            transition(':enter', [
            query('@item', stagger(100, animateChild()), { optional: true })
            ]),
            transition(':leave', [
                style({ transform: 'scale(1)', opacity: 1, height: '*' }),
                animate('1s cubic-bezier(.8, 0.6, 0.2, 1.5)',
                style({
                    transform: 'scale(0.5)', opacity: 0,
                    height: '0px', margin: '0px'
                }))
            ])
        ])
    ];

}
