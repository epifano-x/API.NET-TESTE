import React from 'react';
import './Home.css';

export default function Home() {
  return (
    <div className="container">
      <div className="row">
        <div className="col-md-6 offset-md-3">
          
          <div className="card my-5">

            <form className="card-body cardbody-color p-lg-5">

              <div className="text-center">
                <img src="https://seeklogo.com/images/L/lar-logo-FDA7FE4AE4-seeklogo.com.png" className="img-fluid profile-image-pic  my-3" width="200px" alt="profile"/>
              </div>
              <h2 className="text-center  mt-5">Login</h2>
              <br></br>
              <div className="mb-3">
                <input type="email" className="form-control" id="email" aria-describedby="emailHelp" placeholder="Usuario"/>
              </div>
              <div className="mb-3">
                <input type="password" className="form-control" id="password" placeholder="Senha"/>
              </div>
              <br></br>
              <div className="text-center"><button type="submit" className="btn btn-dark px-5 mb-5 w-50">Login</button></div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
