import { ValidatorFn, AbstractControl } from '@angular/forms';

export function valueSelected(options: any[], key?: any, valueKey?: any): ValidatorFn {
  return (c: AbstractControl): { [key: string]: boolean } | null => {

      const value = valueKey && c.value ? c.value[valueKey] : c.value;
      const found = options.find((item) => item[key] === value);

      if (found) {
          return null;
      } else {
          if (value === '' || value === null) {
              return;
          }
          return { match: true };
      }
  };
}

export function isEmpty(str: any): boolean {
    if (str === null) {
        return true;
    } else if (str === undefined) {
        return true;
    } else if (str.length <= 0) {
        return true;
    } else if (typeof str === 'undefined') {
        return true;
    } else {
        return false;
    }
}

