const requestMoviesType = 'REQUEST_MOVIES';
const receiveMoviesType = 'RECEIVE_MOVIES';
const searchMoviesType = 'SEARCH_MOVIES';
const updateMoviesType = 'UPDATE_MOVIES';
const changeTextToSearchType = 'CHANGE_TEXT_TO_SEARCH';
const initialState = { movies: [], isLoading: false, textToSearch: "" };

export const actionCreators = {
  requestMovies: () => async (dispatch, getState) => {
    if (getState().moviesReducer.isLoading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: requestMoviesType });

    const url = 'api/Movies/list';
    const response = await fetch(url);
    const movies = await response.json();

    dispatch({ type: receiveMoviesType, movies });
  },

  searchMovies: (textToSearch) => async (dispatch, getState) => {
    if (getState().moviesReducer.isLoading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    let url = "";
    if (!textToSearch || textToSearch.trim() === "") {
      dispatch({ type: requestMoviesType });
      url = 'api/Movies/list';
    } else {
      dispatch({ type: searchMoviesType, textToSearch });
      url = `api/Movies?textToSearch=${textToSearch}`;
    }

    const response = await fetch(url);
    const movies = await response.json();

    dispatch({ type: receiveMoviesType, movies });
  },

  changeTextToSearch: (textToSearch) => async (dispatch, getState) => {
    dispatch({ type: changeTextToSearchType, textToSearch });
  },

  markForDeletion: (movieId) => async (dispatch, getState) => {
    const currentMovies = getState().moviesReducer.movies;
    const movie = currentMovies.find(x => x.id === movieId);
    movie.isMarkedForDeletion = true;
    dispatch({ type: updateMoviesType, movies: currentMovies });
  },

  cancelDeletion: (movieId) => async (dispatch, getState) => {
    const currentMovies = getState().moviesReducer.movies;
    const movie = currentMovies.find(x => x.id === movieId);
    movie.isMarkedForDeletion = false;
    dispatch({ type: updateMoviesType, movies: currentMovies });
  },

  deleteMovie: (movieId) => async (dispatch, getState) => {
    const currentMovies = getState().moviesReducer.movies;
    var moviesWithoutDeleted = currentMovies.filter(function(movie, index, arr){
        return movie.id !== movieId;
    });

    const url = `api/Movies/${movieId}`;
    const response = await fetch(url, {
      method: 'DELETE'
    });

    const deleteAnswer = await response.text();

    dispatch({ type: updateMoviesType, movies: moviesWithoutDeleted });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestMoviesType) {
    return {
      ...state,
      isLoading: true,
      textToSearch: ""
    };
  }

  if (action.type === searchMoviesType) {
    return {
      ...state,
      isLoading: true,
      textToSearch: action.textToSearch
    };
  }

  if (action.type === receiveMoviesType) {
    return {
      ...state,
      movies: action.movies,
      isLoading: false
    };
  }

  if (action.type === updateMoviesType) {
    return {
      ...state,
      movies: action.movies
    };
  }

  if (action.type === changeTextToSearchType) {
    return {
      ...state,
      textToSearch: action.textToSearch
      // isLoading: false
    };
  }

  return state;
};
