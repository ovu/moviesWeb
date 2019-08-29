import validationService from "./../services/ValidationService.js";

const changeTitleType = 'CHANGE_TITLE';
const changeDirectorType = 'CHANGE_DIRECTOR';
const changeActorsType = 'CHANGE_ACTORS';
const changeImageType = 'CHANGE_IMAGE';
const changeYearType = 'CHANGE_YEAR';
const savedSuccessfullyType = 'SAVED_SUCCESSFULLY';
const updateJustSavedType = 'UPDATE_JUST_SAVED';
const errorSavingType = 'ERROR_SAVING';

const initialState = {
  title: "", isValidTitle: true,
  director: "", isValidDirector: true,
  actors: "", isValidActors: true,
  year: "", isValidYear: true,
  image: "", isValidImage: true,
  justSaved: false,
  savedMovieAnswer: true
};

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

    if (response.status === 200) {
      const savedMovie = await response.json();
      const savedMovieAnswer = savedMovie && savedMovie.id && savedMovie.id !== "";
      dispatch({type: savedSuccessfullyType, savedMovieAnswer});
    } else {
      dispatch({type: errorSavingType});
    }

  },

  changeTitle: (newTitle) => async (dispatch, getState) => {
    let isValidTitle = validationService.isValidStringWithLength(newTitle, 100);
    dispatch({ type: changeTitleType, title: newTitle, isValidTitle });
  },

  changeDirector: (newDirector) => async (dispatch, getState) => {
    let isValidDirector = validationService.isValidStringWithLength(newDirector, 100);
    dispatch({ type: changeDirectorType, director: newDirector, isValidDirector });
  },

  changeActors: (newActors) => async (dispatch, getState) => {
    let isValidActors = validationService.isValidStringWithLength(newActors, 300);
    dispatch({ type: changeActorsType, actors: newActors, isValidActors });
  },

  changeYear: (newYear) => async (dispatch, getState) => {
    let isValidYear = validationService.isValidYear(newYear);
    dispatch({ type: changeYearType, year: newYear.trim(), isValidYear });
  },

  changeImage: (newImage) => async (dispatch, getState) => {
    let isValidImage = validationService.isValidString(newImage) && validationService.isValidUrl(newImage);
    
    dispatch({ type: changeImageType, image: newImage.trim(), isValidImage });
  },

  updateJustSaved: (newValue) => async (dispatch, getState) => {
    dispatch({ type: updateJustSavedType, justSaved: newValue });
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

  if (action.type === savedSuccessfullyType) {
    return {
      ...state,
      ...initialState,
      justSaved: true,
      savedMovieAnswer: action.savedMovieAnswer
    };
  }

  if (action.type === updateJustSavedType) {
    return {
      ...state,
      justSaved: action.justSaved
    };
  }

  if (action.type === errorSavingType) {
    return {
      ...state,
      justSaved: true,
      savedMovieAnswer: false
    };
  }
  return state;
};
