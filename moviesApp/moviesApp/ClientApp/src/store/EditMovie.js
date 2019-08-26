import validationService from "./../services/ValidationService.js";

const receiveMovieType = 'RECEIVE_MOVIE';
const changeTitleType = 'CHANGE_TITLE';
const changeDirectorType = 'CHANGE_DIRECTOR';
const changeActorsType = 'CHANGE_ACTORS';
const changeImageType = 'CHANGE_IMAGE';
const changeYearType = 'CHANGE_YEAR';
const savedSuccessfullyType = 'SAVED_SUCCESSFULLY';
const updateJustSavedType = 'UPDATE_JUST_SAVED';

const initialState = {
  movieId: "",
  title: "", isValidTitle: true,
  director: "", isValidDirector: true,
  actors: "", isValidActors: true,
  year: "", isValidYear: true,
  image: "", isValidImage: true,
  justSaved: false,
  savedMovieAnswer: true
};

export const actionCreators = {
  requestMovie:(movieId) => async (dispatch, getState) => {
    const url = `api/Movies/${movieId}`;
    const response = await fetch(url);
    const movie = await response.json();
    dispatch({ type: receiveMovieType, title: movie.title, director: movie.director, actors: movie.actors, year: movie.year, image: movie.image, movieId: movieId}); 
  },

  saveMovie: (newMovie, movieId) => async (dispatch, getState) => {

    const url = `api/Movies/${movieId}`;

    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newMovie)
    });

    const savedMovie = await response.text();
    const savedMovieAnswer = savedMovie === "true";

    dispatch({type: savedSuccessfullyType, savedMovieAnswer});
  },

  changeTitle: (newTitle) => async (dispatch, getState) => {
    let isValidTitle = validationService.isValidString(newTitle);
    dispatch({ type: changeTitleType, title: newTitle, isValidTitle });
  },

  changeDirector: (newDirector) => async (dispatch, getState) => {
    let isValidDirector = validationService.isValidString(newDirector);
    dispatch({ type: changeDirectorType, director: newDirector, isValidDirector });
  },

  changeActors: (newActors) => async (dispatch, getState) => {
    let isValidActors = validationService.isValidString(newActors);
    dispatch({ type: changeActorsType, actors: newActors, isValidActors });
  },

  changeYear: (newYear) => async (dispatch, getState) => {
    let isValidYear = validationService.isValidYear(newYear);
    dispatch({ type: changeYearType, year: newYear.trim(), isValidYear });
  },

  changeImage: (newImage) => async (dispatch, getState) => {
    let isValidImage = validationService.isValidString(newImage);
    dispatch({ type: changeImageType, image: newImage.trim(), isValidImage });
  },

  updateJustSaved: (newValue) => async (dispatch, getState) => {
    dispatch({ type: updateJustSavedType, justSaved: newValue });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === receiveMovieType) {
    return {
      ...state,
      movieId: action.movieId,
      title: action.title,
      director: action.director,
      actors: action.actors,
      year: action.year,
      image: action.image
    };
  }

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
  return state;
};
