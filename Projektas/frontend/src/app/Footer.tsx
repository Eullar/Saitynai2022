function Footer() {
  let html =
    <footer className="bg-dark text-center text-lg-start" style={{ color: "white" }}>
      <div className="container p-4">
        <div className="row">
          <div className="col-lg-4">
            <h5 className="text-uppercase">Information</h5>
            <p>
              Car very fast!
            </p>
          </div>
          <div className="col-lg-4">
            <h5 className="text-uppercase">Safety instructions</h5>
            <p>
              If in doubt, flat out
            </p>
          </div>
          <div className="col-lg-4">
            <h5 className="text-uppercase">In case of a crash</h5>
            <p>
              You will pay us!
            </p>
          </div>
        </div>
      </div>
    </footer>

  return html;
}

export default Footer;