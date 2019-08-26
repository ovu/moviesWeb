import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/NewMovie';

class AddNewMovie extends Component {
  render() {
    return (
      <div>
        <h1>Add new movie</h1>
        {renderAddNewMovie(this.props, this.handleTitleChanged, this.handleDirectorChanged, this.handleActorsChanged, 
          this.handleYearChanged, this.handleImageChanged, this.handleSaveMovie)}
      </div>
    );
  }

  handleTitleChanged= (event) => {
    this.props.changeTitle(event.target.value)
  }

  handleDirectorChanged= (event) => {
    this.props.changeDirector(event.target.value)
  }

  handleActorsChanged= (event) => {
    this.props.changeActors(event.target.value)
  }

  handleYearChanged= (event) => {
    this.props.changeYear(event.target.value)
  }
  
  handleImageChanged= (event) => {
    this.props.changeImage(event.target.value)
  }

  handleSaveMovie= (event) => {
    this.props.saveMovie({
      title: this.props.title,
      director: this.props.director,
      actors: this.props.actors,
      year: this.props.year,
      image: this.props.image
    });
  }
}

function renderAddNewMovie(props, handleTitleChanged, handleDirectorChanged, 
  handleActorsChanged, handleYearChanged, handleImageChanged, handleSaveMovie) {
  const canBeSaved = props.isValidTitle && props.isValidDirector && props.isValidActors && props.isValidYear && props.isValidImage &&
    props.title !== "" && props.director !== "" && props.actors !== "" && props.year !== "" && props.image !== "";
  return(
    <div className="container w-75 mb-2 clearfix">
      <div className="row">
        <div className="col">
          <form>
            <div className="form-group">
              <label htmlFor="title">Title</label>
              <input type="text" className="form-control"
                     id="title" aria-describedby="titleHelp" placeholder="Title"
                     value={props.title} onChange={handleTitleChanged}
                     />
                  {
                    props.isValidTitle ? null: 
                    <small id="titleHelp" className="form-text text-danger">Invalid field title</small>
                  }
            </div>
            <div className="form-group">
              <label htmlFor="director">Director</label>
              <input type="text" className="form-control" 
                     id="director" aria-describedby="directorHelp" placeholder="Director"
                     value={props.director} onChange={handleDirectorChanged}
                     />
                  {
                    props.isValidDirector ? null:
                    <small id="directorHelp" className="form-text text-danger">Invalid field director</small>
                  }
            </div>
            <div className="form-group">
              <label htmlFor="actors">Actors</label>
              <input type="text" className="form-control" id="actors" aria-describedby="actorsHelp" placeholder="Actors"
                     value={props.actors} onChange={handleActorsChanged}/>
                  {
                    props.isValidDirector ? null:
                    <small id="actorsHelp" className="form-text text-danger">Invalid field actors</small>
                  }
            </div>
            <div className="form-group">
              <label htmlFor="year">Publication year</label>
              <input type="text" className="form-control" id="year" aria-describedby="yearHelp" placeholder="Year"
                     value={props.year} onChange={handleYearChanged}/>
                  {
                    props.isValidYear ? null:
                    <small id="yearHelp" className="form-text text-danger">Invalid field year. It should be a number between 1900 and current year.</small>
                  }
            </div>
            <div className="form-group">
              <label htmlFor="image">Image</label>
              <input type="text" className="form-control" id="image" aria-describedby="imageHelp" placeholder="Image URL: https://..."
                     value={props.image} onChange={handleImageChanged}/>
                  {
                    props.isValidImage ? null:
                    <small id="imageHelp" className="form-text text-danger">Invalid field image</small>
                  }
            </div>
                  {
                    canBeSaved ?  <button type="submit" className="btn btn-info float-right" onClick={handleSaveMovie}>Save</button>
                    : <button type="submit" className="btn btn-info float-right" disabled title="All fields are required.">Save</button>

                  }
          </form>
        </div>
      </div>
    </div>
  );
}


export default connect(
  state => state.newMovieReducer,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(AddNewMovie);