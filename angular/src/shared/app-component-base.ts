import { Injector, ElementRef } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import {
    LocalizationService,
    PermissionCheckerService,
    FeatureCheckerService,
    NotifyService,
    SettingService,
    MessageService,
    AbpMultiTenancyService
} from 'abp-ng2-module';

import { AppSessionService } from '@shared/session/app-session.service';
import { UrlHelper } from './helpers/UrlHelper';

export abstract class AppComponentBase {

    localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;

    localization: LocalizationService;
    permission: PermissionCheckerService;
    feature: FeatureCheckerService;
    notify: NotifyService;
    setting: SettingService;
    message: MessageService;
    multiTenancy: AbpMultiTenancyService;
    appSession: AppSessionService;
    elementRef: ElementRef;

    getImagePath = UrlHelper.getImagePath;

    constructor(protected injector: Injector) {
        this.localization = injector.get(LocalizationService);
        this.permission = injector.get(PermissionCheckerService);
        this.feature = injector.get(FeatureCheckerService);
        this.notify = injector.get(NotifyService);
        this.setting = injector.get(SettingService);
        this.message = injector.get(MessageService);
        this.multiTenancy = injector.get(AbpMultiTenancyService);
        this.appSession = injector.get(AppSessionService);
        this.elementRef = injector.get(ElementRef);
    }

    l(key: string, ...args: any[]): string {
        let localizedText = this.localization.localize(key, this.localizationSourceName);

        if (!localizedText) {
            localizedText = key;
        }

        if (!args || !args.length) {
            return localizedText;
        }

        args.unshift(localizedText);
        return abp.utils.formatString.apply(this, args);
    }

    isGranted(permissionName: string): boolean {
        return this.permission.isGranted(permissionName);
    }

    isDate(date: any) {
        if (date instanceof Date && date.getTime()) {
            return true;
        }
        return false;
    }

    docso(so) {

        const mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];

        function dochangchuc(so, daydu) {
            let chuoi = '';
            const chuc = Math.floor(so / 10);
            const donvi = so % 10;
            if (chuc > 1) {
                chuoi = ' ' + mangso[chuc] + ' mươi';
                if (donvi == 1) {
                    chuoi += ' mốt';
                }
            } else if (chuc == 1) {
                chuoi = ' mười';
                if (donvi == 1) {
                    chuoi += ' một';
                }
            } else if (daydu && donvi > 0) {
                chuoi = ' lẻ';
            }
            if (donvi == 5 && chuc > 1) {
                chuoi += ' lăm';
            } else if (donvi > 1 || (donvi == 1 && chuc == 0)) {
                chuoi += ' ' + mangso[donvi];
            }
            return chuoi;
        }
        function docblock(so, daydu) {
            let chuoi = '';
            const tram = Math.floor(so / 100);
            so = so % 100;
            if (daydu || tram > 0) {
                chuoi = ' ' + mangso[tram] + ' trăm';
                chuoi += dochangchuc(so, true);
            } else {
                chuoi = dochangchuc(so, false);
            }
            return chuoi;
        }

        function dochangtrieu(so, daydu) {
            let chuoi = '';
            const trieu = Math.floor(so / 1000000);
            so = so % 1000000;
            if (trieu > 0) {
                chuoi = docblock(trieu, daydu) + ' triệu';
                daydu = true;
            }
            const nghin = Math.floor(so / 1000);
            so = so % 1000;
            if (nghin > 0) {
                chuoi += docblock(nghin, daydu) + ' nghìn';
                daydu = true;
            }
            if (so > 0) {
                chuoi += docblock(so, daydu);
            }
            return chuoi;
        }

        if (so == 0) { return mangso[0]; }
        let chuoi = '', hauto = '';
        do {
            const ty = so % 1000000000;
            so = Math.floor(so / 1000000000);
            if (so > 0) {
                chuoi = dochangtrieu(ty, true) + hauto + chuoi;
            } else {
                chuoi = dochangtrieu(ty, false) + hauto + chuoi;
            }
            hauto = ' tỷ';
        } while (so > 0);
        return chuoi;
    }
}
