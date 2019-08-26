const saveMovieType = 'SAVE_MOVIE';
const changeTitleType = 'CHANGE_TITLE';
const changeDirectorType = 'CHANGE_DIRECTOR';
const changeActorsType = 'CHANGE_ACTORS';
const changeImageType = 'CHANGE_IMAGE';
const changeYearType = 'CHANGE_YEAR';
const initialState = {
  title: "", isValidTitle: true,
  director: "", isValidDirector: true,
  actors: "", isValidActors: true,
  year: "", isValidYear: true,
  image: "", isValidImage: true};

export const actionCreators = {
  saveMovie: (newMovie) => async (dispatch, getState) => {

    const url = 'api/Movies';

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newMovie)
    });

    const movies = await response.json();

    dispatch({ type: savedSuccessfullyType});
  },

  changeTitle: (newTitle) => async (dispatch, getState) => {
    let isValidTitle = false;
    if (newTitle && newTitle.trim() !== "") {
      isValidTitle = true;
    }
    dispatch({ type: changeTitleType, title: newTitle, isValidTitle });
  },

  changeDirector: (newDirector) => async (dispatch, getState) => {
    let isValidDirector = false;
    if (newDirector && newDirector.trim() !== "") {
      isValidDirector = true;
    }
    dispatch({ type: changeDirectorType, director: newDirector, isValidDirector });
  },

  changeActors: (newActors) => async (dispatch, getState) => {
    let isValidActors = false;
    if (newActors && newActors.trim() !== "") {
      isValidActors = true;
    }
    dispatch({ type: changeActorsType, actors: newActors, isValidActors });
  },

  changeYear: (newYear) => async (dispatch, getState) => {
    let isValidYear = false;
    const newYearWithoutSpaces = newYear.trim();
    if (newYear && newYearWithoutSpaces !== "") {
      const yearAsNumber = Number(newYearWithoutSpaces);
      if (yearAsNumber === NaN) {
        isValidYear = false;
      } else {
        const currentYear = new Date().getFullYear();
        if (yearAsNumber >= 1900 && yearAsNumber <= currentYear ) {
          isValidYear = true;
        }
      }
    }
    dispatch({ type: changeYearType, year: newYearWithoutSpaces, isValidYear });
  },

  changeImage: (newImage) => async (dispatch, getState) => {
    let isValidImage = false;
    if (newImage && newImage.trim() !== "") {
      isValidImage = true;
    }
    dispatch({ type: changeImageType, image: newImage.trim(), isValidImage });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === changeTitleType) {
    return {
      ...state,
      title: action.title,
      isValidTitle: action.isValidTitle
    };
  }

  if (action.type === changeDirectorType) {
    return {
      ...state,
      director: action.director,
      isValidDirector: action.isValidDirector
    };
  }

  if (action.type === changeActorsType) {
    return {
      ...state,
      actors: action.actors,
      isValidActors: action.isValidActors
    };
  }

  if (action.type === changeYearType) {
    return {
      ...state,
      year: action.year,
      isValidYear: action.isValidYear
    };
  }

  if (action.type === changeImageType) {
    return {
      ...state,
      image: action.image,
      isValidImage: action.isValidImage
    };
  }

  return state;
};