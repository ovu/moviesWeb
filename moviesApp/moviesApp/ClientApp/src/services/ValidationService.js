const ValidationService = {
  isValidYear: function(newYear) {
    let isValidYear = false;
    const newYearWithoutSpaces = newYear.trim();
    if (newYear && newYearWithoutSpaces !== "") {
      const yearAsNumber = Number(newYearWithoutSpaces);
      if (isNaN(yearAsNumber)) {
        isValidYear = false;
      } else {
        const currentYear = new Date().getFullYear();
        if (yearAsNumber >= 1900 && yearAsNumber <= currentYear ) {
          isValidYear = true;
        }
      }
    }

    return isValidYear;
  },

  isValidString: function(value) {
    let isValid = false;
    if (value && value.trim() !== "") {
      isValid = true;
    }
    return isValid;
  }
};

export default ValidationService;
